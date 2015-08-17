using System;
using System.Runtime.InteropServices;

// ReSharper disable InconsistentNaming


namespace WinApiWrapper.Native.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct KEYBDINPUT
    {
        internal Enums.VirtualKeys wVk;
        internal Enums.ScanCode wScan;
        internal Enums.KeyEventF dwFlags;
        internal int time;
        internal UIntPtr dwExtraInfo;
    }
}
