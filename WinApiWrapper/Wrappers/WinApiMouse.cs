using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Runtime.InteropServices;
using WinApiWrapper.Enums;
using WinApiWrapper.Interfaces;
using WinApiWrapper.Internal.Hooks;
using WinApiWrapper.Internal.Mappers;
using WinApiWrapper.Native.Constants;
using WinApiWrapper.Native.Delegates;
using WinApiWrapper.Native.Enums;
using WinApiWrapper.Native.Methods;
using WinApiWrapper.Native.Structs;


namespace WinApiWrapper.Wrappers
{
    public class WinApiMouse : IWinApiMouse
    {
        private readonly MouseMessagesTranslator _messageTranslator;

        private GCHandle _mouseHookProcGcHandle;
        private readonly IntPtr _mouseHookHandle;

        private readonly ConcurrentDictionary<Guid, WeakReference<ButtonHook>> _buttonHooks;
        private readonly ConcurrentDictionary<Guid, WeakReference<MoveHook>> _moveHooks;
        private readonly ConcurrentDictionary<Guid, WeakReference<WheelHook>> _wheelHooks;


        public WinApiMouse()
        {
            _messageTranslator = new MouseMessagesTranslator();

            _buttonHooks = new ConcurrentDictionary<Guid, WeakReference<ButtonHook>>();
            _moveHooks = new ConcurrentDictionary<Guid, WeakReference<MoveHook>>();
            _wheelHooks = new ConcurrentDictionary<Guid, WeakReference<WheelHook>>();

            HookProc mouseHookProc = HookWindowProc;
            _mouseHookProcGcHandle = GCHandle.Alloc(mouseHookProc);
            _mouseHookHandle = User32.SetWindowsHookEx(HookType.WH_MOUSE_LL, mouseHookProc, IntPtr.Zero, IntPtr.Zero);
        }

        ~WinApiMouse()
        {
            _mouseHookProcGcHandle.Free();
        }


        #region Getters
        public Point Position
        {
            get
            {
                CURSORINFO pci;
                pci.cbSize = Marshal.SizeOf(typeof(CURSORINFO));
                User32.GetCursorInfo(out pci);
                return new Point(pci.ptScreenPos.x, pci.ptScreenPos.y);
            }
        }

        public int X
        {
            get { return Position.X; }
            set { User32.SetCursorPos(value, Position.Y); }
        }

        public int Y
        {
            get { return Position.Y; }
            set { User32.SetCursorPos(Position.X, value); }
        }

        public bool IsLeftButtonDown => User32.GetAsyncKeyState(VirtualKeys.LBUTTON) < 0;

        public bool IsMiddleButtonDown => User32.GetAsyncKeyState(VirtualKeys.MBUTTON) < 0;

        public bool IsRightButtonDown => User32.GetAsyncKeyState(VirtualKeys.RBUTTON) < 0;

        public Point GetClientPosition(IWinApiWindow clientWindow)
        {
            return GetClientPosition(clientWindow.Hwnd);
        }

        public Point GetClientPosition(IntPtr hwnd)
        {
            CURSORINFO pci;
            pci.cbSize = Marshal.SizeOf(typeof(CURSORINFO));
            User32.GetCursorInfo(out pci);

            User32.ScreenToClient(hwnd, ref pci.ptScreenPos);
            return new Point(pci.ptScreenPos.x, pci.ptScreenPos.y);
        }
        #endregion Getters


        #region Setters
        public bool IsVisible
        {
            get
            {
                CURSORINFO pci;
                pci.cbSize = Marshal.SizeOf(typeof(CURSORINFO));
                User32.GetCursorInfo(out pci);
                return pci.flags == Others.CURSOR_SHOWING;
            }
            set { User32.ShowCursor(value); }
        }

        public void SetPosition(Point position)
        {
            User32.SetCursorPos(position.X, position.Y);
        }

        public void SetX(int x)
        {
            User32.SetCursorPos(x, Position.Y);
        }

        public void SetY(int y)
        {
            User32.SetCursorPos(Position.X, y);
        }

        public void Show()
        {
            User32.ShowCursor(true);
        }

        public void Hide()
        {
            User32.ShowCursor(false);
        }
        #endregion Setters


        #region Actions
        public void PerformClick(MouseButton mouseButton, Point position)
        {
            var mouseEventMessages = _messageTranslator.GetMouseEventFSequence(mouseButton);
            if (mouseEventMessages != null)
            {
                var savedPosition = Position;
                SetPosition(position);
                User32.mouse_event(mouseEventMessages.Item1, 0, 0, 0, 0);
                User32.mouse_event(mouseEventMessages.Item2, 0, 0, 0, 0);
                SetPosition(savedPosition);
            }
        }

        public void PerformClick(MouseButton mouseButton, int x, int y)
        {
            PerformClick(mouseButton, new Point(x, y));
        }

        public void PerformClick(MouseButton mouseButton, Point position, IntPtr hwnd)
        {
            var windowMessages = _messageTranslator.GetWindowMessageSequence(mouseButton);
            User32.SendMessage(hwnd, (uint)windowMessages.Item1, new IntPtr(1),
                               new IntPtr(position.Y * 65536 + position.X));
            User32.SendMessage(hwnd, (uint)windowMessages.Item2, new IntPtr(0),
                               new IntPtr(position.Y * 65536 + position.X));
        }

        public void PerformClick(MouseButton mouseButton, int x, int y, IntPtr hwnd)
        {
            PerformClick(mouseButton, new Point(x, y), hwnd);
        }
        #endregion Actions


        #region Hooks
        public Guid RegisterMoveHook(Action<Point> hookMethod)
        {
            return RegisterHook(_moveHooks, new WeakReference<MoveHook>(new MoveHook(hookMethod)));
        }

        public bool UnregisterMoveHook(Guid hookGuid)
        {
            return UnregisterHook(_moveHooks, hookGuid);
        }

        public void UnregisterAllMoveHooks()
        {
            _moveHooks.Clear();
        }

        public Guid RegisterButtonHook(MouseButtonAction buttonAction, Action<MouseButton> hookMethod)
        {
            return RegisterHook(_buttonHooks, new WeakReference<ButtonHook>(new ButtonHook(buttonAction, hookMethod)));
        }

        public bool UnregisterButtonHook(Guid hookGuid)
        {
            return UnregisterHook(_buttonHooks, hookGuid);
        }

        public void UnregisterAllButtonHooks()
        {
            _buttonHooks.Clear();
        }

        public Guid RegisterWheelHook(MouseWheelOrientation wheelOrientation, Action<int> hookMethod)
        {
            return RegisterHook(_wheelHooks, new WeakReference<WheelHook>(new WheelHook(wheelOrientation, hookMethod)));
        }

        public bool UnregisterWheelHook(Guid hookGuid)
        {
            return UnregisterHook(_wheelHooks, hookGuid);
        }

        public void UnregisterAllWheelHooks()
        {
            _wheelHooks.Clear();
        }

        public void UnregisterAllHooks()
        {
            UnregisterAllMoveHooks();
            UnregisterAllButtonHooks();
            UnregisterAllWheelHooks();
        }
        #endregion Hooks
        

        #region Hook Procedure
        private IntPtr HookWindowProc(int code, IntPtr wparam, IntPtr lparam)
        {
            if (code >= 0)
            {
                if (IsMoveMessage(wparam))
                {
                    InvokeMoveHooks(wparam, lparam);
                }
                else if (IsWheelMessage(wparam))
                {
                    InvokeWheelHooks(wparam, lparam);
                }
                else if (IsButtonMessage(wparam))
                {
                    InvokeButtonHooks(wparam, lparam);
                }
            }
            return User32.CallNextHookEx(_mouseHookHandle, code, wparam, lparam);
        }
        #endregion Hook Procedure


        #region Private Methods
        private static Guid RegisterHook<THook>(ConcurrentDictionary<Guid, WeakReference<THook>> hookList,
                                                WeakReference<THook> hook)
            where THook : class
        {
            var guid = Guid.NewGuid();
            hookList.TryAdd(guid, hook);
            return guid;
        }

        private static bool UnregisterHook<THook>(ConcurrentDictionary<Guid, WeakReference<THook>> hookList,
                                                  Guid hookGuid)
            where THook : class
        {
            WeakReference<THook> removedHook;
            return hookList.TryRemove(hookGuid, out removedHook);
        }

        private void InvokeMoveHooks(IntPtr wparam, IntPtr lparam)
        {
            var mouseHookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lparam, typeof(MSLLHOOKSTRUCT));
            var mousePosition = new Point(mouseHookStruct.pt.x, mouseHookStruct.pt.y);

            foreach (var moveHookWeakReference in _moveHooks.Values)
            {
                MoveHook moveHook;
                moveHookWeakReference.TryGetTarget(out moveHook);

                moveHook?.HookMethod?.Invoke(mousePosition);
            }
        }

        private void InvokeWheelHooks(IntPtr wparam, IntPtr lparam)
        {
            var message = (WindowMessage)wparam;
            MouseWheelOrientation wheelOrientation = _messageTranslator.GetWheelOrientation(message);

            var mouseHookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lparam, typeof(MSLLHOOKSTRUCT));
            var mouseDelta = mouseHookStruct.mouseData >> 16;

            foreach (var wheelHookWeakReference in _wheelHooks.Values)
            {
                WheelHook wheelHook;
                wheelHookWeakReference.TryGetTarget(out wheelHook);

                if (wheelHook?.WheelOrientation == wheelOrientation)
                {
                    wheelHook.HookMethod?.Invoke(mouseDelta);
                }
            }
        }

        private void InvokeButtonHooks(IntPtr wparam, IntPtr lparam)
        {
            var message = (WindowMessage)wparam;
            MouseButtonAction action = _messageTranslator.GetMouseButtonAction(message);
            MouseButton mouseButton = _messageTranslator.GetMouseButton(message);

            if (action != MouseButtonAction.None && mouseButton != MouseButton.None)
            {
                foreach (var buttonHookWeakReference in _buttonHooks.Values)
                {
                    ButtonHook buttonHook;
                    buttonHookWeakReference.TryGetTarget(out buttonHook);

                    if (buttonHook?.ButtonAction == action)
                    {
                        buttonHook.HookMethod?.Invoke(mouseButton);
                    }
                }
            }
        }

        private static bool IsButtonMessage(IntPtr wparam)
        {
            var message = (WindowMessage)wparam;
            return message == WindowMessage.LBUTTONDOWN ||
                   message == WindowMessage.RBUTTONDOWN ||
                   message == WindowMessage.MBUTTONDOWN ||
                   message == WindowMessage.LBUTTONUP ||
                   message == WindowMessage.RBUTTONUP ||
                   message == WindowMessage.MBUTTONUP ||
                   message == WindowMessage.LBUTTONDBLCLK ||
                   message == WindowMessage.RBUTTONDBLCLK ||
                   message == WindowMessage.MBUTTONDBLCLK;
        }

        private static bool IsWheelMessage(IntPtr wparam)
        {
            var message = (WindowMessage)wparam;
            return message == WindowMessage.MOUSEWHEEL || message == WindowMessage.MOUSEHWHEEL;
        }

        private static bool IsMoveMessage(IntPtr wparam)
        {
            var message = (WindowMessage)wparam;
            return message == WindowMessage.MOUSEMOVE;
        }
        #endregion Private Methods
    }
}
