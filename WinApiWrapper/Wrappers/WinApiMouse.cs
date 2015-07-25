using System;
using System.Drawing;
using System.Runtime.InteropServices;
using WinApiWrapper.Enums;
using WinApiWrapper.Interfaces;
using WinApiWrapper.Mappers;
using WinApiWrapper.Unsafe;

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
                NativeMethods.Structs.CURSORINFO pci;
                pci.cbSize = Marshal.SizeOf(typeof(NativeMethods.Structs.CURSORINFO));
                NativeMethods.User32.GetCursorInfo(out pci);
                return new Point(pci.ptScreenPos.x, pci.ptScreenPos.y);
            }
        }

        public int X
        {
            get { return Position.X; }
        }

        public int Y
        {
            get { return Position.Y; }
            set { NativeMethods.User32.SetCursorPos(Position.X, value); }
        }

        public bool IsLeftButtonDown
        {
            get { return NativeMethods.User32.GetAsyncKeyState(NativeMethods.Enums.VirtualKeys.LBUTTON) < 0; }
        }

        public bool IsRightButtonDown
        {
            get { return NativeMethods.User32.GetAsyncKeyState(NativeMethods.Enums.VirtualKeys.RBUTTON) < 0; }
        }

        public Point GetClientPosition(IWinApiWindow clientWindow)
        {
            return GetClientPosition(clientWindow.Hwnd);
        }

        public Point GetClientPosition(IntPtr hwnd)
        {
            NativeMethods.Structs.CURSORINFO pci;
            pci.cbSize = Marshal.SizeOf(typeof(NativeMethods.Structs.CURSORINFO));
            NativeMethods.User32.GetCursorInfo(out pci);

            NativeMethods.User32.ScreenToClient(hwnd, ref pci.ptScreenPos);
            return new Point(pci.ptScreenPos.x, pci.ptScreenPos.y);
        }


        // Setters
        public bool IsVisible
        {
            get
            {
                NativeMethods.Structs.CURSORINFO pci;
                pci.cbSize = Marshal.SizeOf(typeof(NativeMethods.Structs.CURSORINFO));
                NativeMethods.User32.GetCursorInfo(out pci);
                return pci.flags == NativeMethods.Constants.CURSOR_SHOWING;
            }
            set { NativeMethods.User32.ShowCursor(value); }
        }

        public void SetPosition(Point position)
        {
            NativeMethods.User32.SetCursorPos(position.X, position.Y);
        }

        public void SetX(int x)
        {
            NativeMethods.User32.SetCursorPos(x, Position.Y);
        }

        public void SetY(int y)
        {
            NativeMethods.User32.SetCursorPos(Position.X, y);
        }

        public void Show()
        {
            NativeMethods.User32.ShowCursor(true);
        }

        public void Hide()
        {
            NativeMethods.User32.ShowCursor(false);
        }


        // Actions
        public void PerformClick(WinApiMouseButton mouseButton, Point position)
        {
            var mouseEventMessages = _mouseEventMapper.Map(mouseButton);
            if (mouseEventMessages != null)
            {
                var savedPosition = Position;
                SetPosition(position);
                NativeMethods.User32.mouse_event(mouseEventMessages.Item1, 0, 0, 0, 0);
                NativeMethods.User32.mouse_event(mouseEventMessages.Item2, 0, 0, 0, 0);
                SetPosition(savedPosition);
            }
        }

        public void PerformClick(WinApiMouseButton mouseButton, int x, int y)
        {
            PerformClick(mouseButton, new Point(x, y));
        }

        public void PerformClick(WinApiMouseButton mouseButton, IntPtr hwnd, Point position)
        {
            var windowMessages = _mouseWindowMapper.Map(mouseButton);
            NativeMethods.User32.SendMessage(hwnd, (uint)windowMessages.Item1, new IntPtr(1),
                                             new IntPtr(position.Y * 65536 + position.X));
            NativeMethods.User32.SendMessage(hwnd, (uint)windowMessages.Item2, new IntPtr(0),
                                             new IntPtr(position.Y * 65536 + position.X));
        }

        public void PerformClick(WinApiMouseButton mouseButton, IntPtr hwnd, int x, int y)
        {
            PerformClick(mouseButton, hwnd, new Point(x, y));
        }
    }
}
