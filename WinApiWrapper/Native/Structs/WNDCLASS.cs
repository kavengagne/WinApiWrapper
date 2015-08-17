using System;
using System.Runtime.InteropServices;
using WinApiWrapper.Native.Delegates;


namespace WinApiWrapper.Native.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct WNDCLASS
    {
        public Enums.ClassStyles style;
        [MarshalAs(UnmanagedType.FunctionPtr)] public WndProc lpfnWndProc;
        public int cbClsExtra;
        public int cbWndExtra;
        public IntPtr hInstance;
        public IntPtr hIcon;
        public IntPtr hCursor;
        public IntPtr hbrBackground;
        [MarshalAs(UnmanagedType.LPTStr)] public string lpszMenuName;
        [MarshalAs(UnmanagedType.LPTStr)] public string lpszClassName;
    }
}