using System;
using System.Runtime.InteropServices;


namespace WinApiWrapper.Native.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MOUSEINPUT
    {
        internal int dx;
        internal int dy;
        internal int mouseData;
        internal Enums.MouseEventF dwFlags;
        internal uint time;
        internal UIntPtr dwExtraInfo;
    }
}