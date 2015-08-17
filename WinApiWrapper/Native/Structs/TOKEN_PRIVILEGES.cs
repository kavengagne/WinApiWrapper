using System.Runtime.InteropServices;
using WinApiWrapper.Native.Constants;


namespace WinApiWrapper.Native.Structs
{
    public struct TOKEN_PRIVILEGES
    {
        public uint PrivilegeCount;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Others.ANYSIZE_ARRAY)]
        public LUID_AND_ATTRIBUTES[] Privileges;
    }
}