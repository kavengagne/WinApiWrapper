using System.Runtime.InteropServices;
using WinApiWrapper.Native.Enums;
using WinApiWrapper.Native.Structs;


namespace WinApiWrapper.Native.Methods
{
    public partial class User32
    {
        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(VirtualKeys vKey);

        [DllImport("user32.dll")]
        public static extern int ShowCursor(bool bShow);
        
        [DllImport("user32.dll")]
        public static extern bool GetCursorInfo(out CURSORINFO pci);
        
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        public static extern uint SendInput(
            uint nInputs, [MarshalAs(UnmanagedType.LPArray), In] INPUT[] pInputs, int cbSize);

        [DllImport("user32.dll")]
        public static extern void mouse_event(MouseEventF dwFlags, uint dx, uint dy, uint dwData,
                                              int dwExtraInfo);
    }
}