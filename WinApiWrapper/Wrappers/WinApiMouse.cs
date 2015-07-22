using System;
using System.Drawing;
using System.Runtime.InteropServices;
using WinApiWrapper.Interfaces;
using WinApiWrapper.Unsafe;

namespace WinApiWrapper.Wrappers
{
    public class WinApiMouse : IWinApiMouse
    {
        public WinApiMouse()
        {
        }

        public Point Position
        {
            get
            {
                // TODO: KG - These are Screen Coordinates. May need to convert to Client Coordinates.
                // TODO: KG - Create a ClientPosition property ???
                NativeMethods.Structs.CURSORINFO pci;
                pci.cbSize = Marshal.SizeOf(typeof(NativeMethods.Structs.CURSORINFO));
                NativeMethods.User32.GetCursorInfo(out pci);

                return new Point(pci.ptScreenPos.x, pci.ptScreenPos.y);
            }
            set
            {
                // TODO: KG - Set Mouse Position.
            }
        }

        public bool IsVisible
        {
            get
            {
                NativeMethods.Structs.CURSORINFO pci;
                pci.cbSize = Marshal.SizeOf(typeof(NativeMethods.Structs.CURSORINFO));
                NativeMethods.User32.GetCursorInfo(out pci);
                return pci.flags == NativeMethods.Constants.CURSOR_SHOWING;
            }
            set
            {
                NativeMethods.User32.ShowCursor(value);
            }
        }

        public int X
        {
            get { return Position.X; }
            set
            {
                // TODO: Set X Position
                // Position.X = value;
            }
        }

        public int Y
        {
            get { return Position.Y; }
            set
            {
                // TODO: Set Y Position
                // Position.Y = value;
            }
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
    }
}
