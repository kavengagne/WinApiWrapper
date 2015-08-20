using System;
using System.Runtime.InteropServices;


namespace WinApiWrapper.Native.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MSLLHOOKSTRUCT
    {
        public POINT pt;
        public int mouseData; // be careful, this must be ints, not uints.
        public int flags;
        public int time;
        public UIntPtr dwExtraInfo;
    }
}