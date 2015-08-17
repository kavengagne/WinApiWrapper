using System;
using System.Runtime.InteropServices;
using System.Security.Principal;

// ReSharper disable InconsistentNaming


namespace WinApiWrapper.Native.Methods
{
    public class Advapi32
    {
        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool OpenProcessToken(IntPtr ProcessHandle, TokenAccessLevels DesiredAccess,
                                                   out IntPtr TokenHandle);

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool LookupPrivilegeValue(string lpSystemName, string lpName,
                                                       Structs.LUID lpLuid);

        // Use this signature if you do not want the previous state

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AdjustTokenPrivileges(IntPtr TokenHandle,
                                                        [MarshalAs(UnmanagedType.Bool)] bool DisableAllPrivileges,
                                                        ref Structs.TOKEN_PRIVILEGES NewState,
                                                        uint Zero,
                                                        IntPtr Null1,
                                                        IntPtr Null2);

        // Use this signature if you want the previous state information returned

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AdjustTokenPrivileges(IntPtr TokenHandle,
                                                        [MarshalAs(UnmanagedType.Bool)] bool DisableAllPrivileges,
                                                        ref Structs.TOKEN_PRIVILEGES NewState,
                                                        uint BufferLengthInBytes,
                                                        ref Structs.TOKEN_PRIVILEGES PreviousState,
                                                        out uint ReturnLengthInBytes);
    }
}
