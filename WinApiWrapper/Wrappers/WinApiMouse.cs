using System;
using System.Drawing;
using System.Runtime.InteropServices;
using WinApiWrapper.Enums;
using WinApiWrapper.Interfaces;
using WinApiWrapper.Mappers;
using WinApiWrapper.Native.Constants;
using WinApiWrapper.Native.Enums;
using WinApiWrapper.Native.Methods;
using WinApiWrapper.Native.Structs;


namespace WinApiWrapper.Wrappers
{
    public class WinApiMouse : IWinApiMouse
    {
        private readonly MouseEventMessagesMapper _mouseEventMapper;
        private readonly MouseWindowMessagesMapper _mouseWindowMapper;

        public WinApiMouse()
        {
            _mouseEventMapper = new MouseEventMessagesMapper();
            _mouseWindowMapper = new MouseWindowMessagesMapper();
        }

        // Getters
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


        // Setters
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


        // Actions
        public void PerformClick(WinApiMouseButton mouseButton, Point position)
        {
            var mouseEventMessages = _mouseEventMapper.Map(mouseButton);
            if (mouseEventMessages != null)
            {
                var savedPosition = Position;
                SetPosition(position);
                User32.mouse_event(mouseEventMessages.Item1, 0, 0, 0, 0);
                User32.mouse_event(mouseEventMessages.Item2, 0, 0, 0, 0);
                SetPosition(savedPosition);
            }
        }

        public void PerformClick(WinApiMouseButton mouseButton, int x, int y)
        {
            PerformClick(mouseButton, new Point(x, y));
        }

        public void PerformClick(WinApiMouseButton mouseButton, Point position, IntPtr hwnd)
        {
            var windowMessages = _mouseWindowMapper.Map(mouseButton);
            User32.SendMessage(hwnd, (uint)windowMessages.Item1, new IntPtr(1),
                                             new IntPtr(position.Y * 65536 + position.X));
            User32.SendMessage(hwnd, (uint)windowMessages.Item2, new IntPtr(0),
                                             new IntPtr(position.Y * 65536 + position.X));
        }

        public void PerformClick(WinApiMouseButton mouseButton, int x, int y, IntPtr hwnd)
        {
            PerformClick(mouseButton, new Point(x, y), hwnd);
        }
    }
}
