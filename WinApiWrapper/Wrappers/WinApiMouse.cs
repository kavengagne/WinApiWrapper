using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using WinApiWrapper.Enums;
using WinApiWrapper.Interfaces;
using WinApiWrapper.Native.Constants;
using WinApiWrapper.Native.Delegates;
using WinApiWrapper.Native.Enums;
using WinApiWrapper.Native.Methods;
using WinApiWrapper.Native.Structs;


namespace WinApiWrapper.Wrappers
{
    public static class WinApiMouse
    {
        private static readonly Internal.Mappers.MouseMessagesTranslator MessageTranslator;
        private static readonly ConcurrentDictionary<Guid, Internal.Hooks.MouseButtonHook> ButtonHooks;
        //private static readonly ConcurrentDictionary<Guid, Internal.Hooks.MoveHook> MoveHooks;
        private static readonly ConcurrentDictionary<Guid, Internal.Hooks.WheelHook> WheelHooks;
        private static readonly HookHandle MouseHookHandle;

        static WinApiMouse()
        {
            MessageTranslator = new Internal.Mappers.MouseMessagesTranslator();

            ButtonHooks = new ConcurrentDictionary<Guid, Internal.Hooks.MouseButtonHook>();
            //MoveHooks = new ConcurrentDictionary<Guid, Internal.Hooks.MoveHook>();
            WheelHooks = new ConcurrentDictionary<Guid, Internal.Hooks.WheelHook>();

            //MouseHookHandle = new HookHandle(HookType.WH_MOUSE_LL, HookWindowProc);
        }


        #region Getters
        public static Point Position
        {
            get
            {
                CURSORINFO pci;
                pci.cbSize = Marshal.SizeOf(typeof(CURSORINFO));
                User32.GetCursorInfo(out pci);
                return new Point(pci.ptScreenPos.x, pci.ptScreenPos.y);
            }
        }

        public static int X
        {
            get { return Position.X; }
            set { User32.SetCursorPos(value, Position.Y); }
        }

        public static int Y
        {
            get { return Position.Y; }
            set { User32.SetCursorPos(Position.X, value); }
        }

        public static bool IsLeftButtonDown => User32.GetAsyncKeyState(VirtualKeys.LBUTTON) < 0;

        public static bool IsMiddleButtonDown => User32.GetAsyncKeyState(VirtualKeys.MBUTTON) < 0;

        public static bool IsRightButtonDown => User32.GetAsyncKeyState(VirtualKeys.RBUTTON) < 0;

        public static Point GetClientPosition(IWinApiWindow clientWindow)
        {
            return GetClientPosition(clientWindow.Hwnd);
        }

        public static Point GetClientPosition(IntPtr hwnd)
        {
            CURSORINFO pci;
            pci.cbSize = Marshal.SizeOf(typeof(CURSORINFO));
            User32.GetCursorInfo(out pci);

            User32.ScreenToClient(hwnd, ref pci.ptScreenPos);
            return new Point(pci.ptScreenPos.x, pci.ptScreenPos.y);
        }
        #endregion Getters


        #region Setters
        public static bool IsVisible
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

        public static void SetPosition(Point position)
        {
            User32.SetCursorPos(position.X, position.Y);
        }

        public static void SetX(int x)
        {
            User32.SetCursorPos(x, Position.Y);
        }

        public static void SetY(int y)
        {
            User32.SetCursorPos(Position.X, y);
        }

        public static void Show()
        {
            User32.ShowCursor(true);
        }

        public static void Hide()
        {
            User32.ShowCursor(false);
        }
        #endregion Setters


        #region Actions
        public static void PerformClick(MouseButton mouseButton, Point position)
        {
            var mouseEventMessages = MessageTranslator.GetMouseEventFSequence(mouseButton);
            if (mouseEventMessages != null)
            {
                var savedPosition = Position;
                SetPosition(position);
                User32.mouse_event(mouseEventMessages.Item1, 0, 0, 0, 0);
                User32.mouse_event(mouseEventMessages.Item2, 0, 0, 0, 0);
                SetPosition(savedPosition);
            }
        }

        public static void PerformClick(MouseButton mouseButton, int x, int y)
        {
            PerformClick(mouseButton, new Point(x, y));
        }

        public static void PerformClick(MouseButton mouseButton, Point position, IntPtr hwnd)
        {
            var windowMessages = MessageTranslator.GetWindowMessageSequence(mouseButton);
            User32.SendMessage(hwnd, (uint)windowMessages.Item1, new IntPtr(1),
                               new IntPtr(position.Y * 65536 + position.X));
            User32.SendMessage(hwnd, (uint)windowMessages.Item2, new IntPtr(0),
                               new IntPtr(position.Y * 65536 + position.X));
        }

        public static void PerformClick(MouseButton mouseButton, int x, int y, IntPtr hwnd)
        {
            PerformClick(mouseButton, new Point(x, y), hwnd);
        }
        #endregion Actions


        #region Hooks
        //public static Guid RegisterMoveHook(Action<Point> hookMethod)
        //{
        //    return RegisterHook(MoveHooks, new Internal.Hooks.MoveHook(hookMethod));
        //}

        //public static bool UnregisterMoveHook(Guid hookGuid)
        //{
        //    return UnregisterHook(MoveHooks, hookGuid);
        //}

        //public static void UnregisterAllMoveHooks()
        //{
        //    MoveHooks.Clear();
        //}

        public static Guid RegisterButtonHook(MouseButtonAction buttonAction, Action<MouseButton> hookMethod)
        {
            return RegisterHook(ButtonHooks, new Internal.Hooks.MouseButtonHook(buttonAction, hookMethod));
        }

        public static bool UnregisterButtonHook(Guid hookGuid)
        {
            return UnregisterHook(ButtonHooks, hookGuid);
        }

        public static void UnregisterAllButtonHooks()
        {
            ButtonHooks.Clear();
        }

        public static Guid RegisterWheelHook(MouseWheelOrientation wheelOrientation, Action<int> hookMethod)
        {
            return RegisterHook(WheelHooks, new Internal.Hooks.WheelHook(wheelOrientation, hookMethod));
        }

        public static bool UnregisterWheelHook(Guid hookGuid)
        {
            return UnregisterHook(WheelHooks, hookGuid);
        }

        public static void UnregisterAllWheelHooks()
        {
            WheelHooks.Clear();
        }

        public static void UnregisterAllHooks()
        {
            //UnregisterAllMoveHooks();
            UnregisterAllButtonHooks();
            UnregisterAllWheelHooks();
        }
        #endregion Hooks


        #region Hook Procedure
        private static IntPtr HookWindowProc(int code, IntPtr wparam, IntPtr lparam)
        {
            if (code >= 0)
            {
                //if (IsMoveMessage(wparam))
                //{
                //    InvokeMoveHooks(wparam, lparam);
                //}
                if (IsWheelMessage(wparam))
                {
                    InvokeWheelHooks(wparam, lparam);
                }
                else if (IsButtonMessage(wparam))
                {
                    InvokeButtonHooks(wparam, lparam);
                }
            }
            return User32.CallNextHookEx(MouseHookHandle, code, wparam, lparam);
        }
        #endregion Hook Procedure


        #region Private Methods
        private static Guid RegisterHook<THook>(ConcurrentDictionary<Guid, THook> hookList, THook hook)
        {
            var guid = Guid.NewGuid();
            hookList.TryAdd(guid, hook);
            return guid;
        }

        private static bool UnregisterHook<THook>(ConcurrentDictionary<Guid, THook> hookList, Guid hookGuid)
        {
            THook removedHook;
            return hookList.TryRemove(hookGuid, out removedHook);
        }

        //private static void InvokeMoveHooks(IntPtr wparam, IntPtr lparam)
        //{
        //    var mouseHookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lparam, typeof(MSLLHOOKSTRUCT));
        //    var mousePosition = new Point(mouseHookStruct.pt.x, mouseHookStruct.pt.y);

        //    foreach (var moveHook in MoveHooks.Values)
        //    {
        //        moveHook.HookMethod?.Invoke(mousePosition);
        //    }
        //}

        private static void InvokeWheelHooks(IntPtr wparam, IntPtr lparam)
        {
            var message = (WindowMessage)wparam;
            MouseWheelOrientation wheelOrientation = MessageTranslator.GetWheelOrientation(message);

            var mouseHookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lparam, typeof(MSLLHOOKSTRUCT));
            var mouseDelta = mouseHookStruct.mouseData >> 16;

            foreach (var wheelHook in WheelHooks.Values.Where(hook => hook.WheelOrientation == wheelOrientation))
            {
                wheelHook.HookMethod?.Invoke(mouseDelta);
            }
        }

        private static void InvokeButtonHooks(IntPtr wparam, IntPtr lparam)
        {
            var message = (WindowMessage)wparam;
            MouseButtonAction action = MessageTranslator.GetMouseButtonAction(message);
            MouseButton mouseButton = MessageTranslator.GetMouseButton(message);

            if (action != MouseButtonAction.None && mouseButton != MouseButton.None)
            {
                foreach (var buttonHook in ButtonHooks.Values.Where(hook => hook.ButtonAction == action))
                {
                    buttonHook.HookMethod?.Invoke(mouseButton);
                }
            }
        }

        private static bool IsButtonMessage(IntPtr wparam)
        {
            var message = (WindowMessage)wparam;
            switch (message)
            {
                case WindowMessage.LBUTTONDOWN:
                case WindowMessage.RBUTTONDOWN:
                case WindowMessage.MBUTTONDOWN:
                case WindowMessage.LBUTTONUP:
                case WindowMessage.RBUTTONUP:
                case WindowMessage.MBUTTONUP:
                case WindowMessage.LBUTTONDBLCLK:
                case WindowMessage.RBUTTONDBLCLK:
                case WindowMessage.MBUTTONDBLCLK:
                    return true;
                default:
                    return false;
            }
        }

        private static bool IsWheelMessage(IntPtr wparam)
        {
            var message = (WindowMessage)wparam;
            return message == WindowMessage.MOUSEWHEEL || message == WindowMessage.MOUSEHWHEEL;
        }

        //private static bool IsMoveMessage(IntPtr wparam)
        //{
        //    var message = (WindowMessage)wparam;
        //    return message == WindowMessage.MOUSEMOVE;
        //}
        #endregion Private Methods
    }
}
