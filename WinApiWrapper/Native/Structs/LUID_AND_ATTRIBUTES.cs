using System.Runtime.InteropServices;


namespace WinApiWrapper.Native.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct LUID_AND_ATTRIBUTES
    {
        public LUID Luid;
        public uint Attributes;
    }
}