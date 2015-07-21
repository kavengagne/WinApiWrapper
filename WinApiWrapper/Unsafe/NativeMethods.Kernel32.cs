using System;
using System.Runtime.InteropServices;

// ReSharper disable InconsistentNaming

namespace WinApiWrapper.Unsafe
{
    public static partial class NativeMethods
    {
        public class Kernel32
        {
            [DllImport("kernel32.dll")]
            public static extern IntPtr GetCurrentProcess();

            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern IntPtr CreateToolhelp32Snapshot(Enums.SnapshotFlags dwFlags, uint th32ProcessID);

            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool CloseHandle(IntPtr hHandle);

            [DllImport("kernel32.dll")]
            public static extern bool Process32Next(IntPtr hSnapshot, ref Structs.PROCESSENTRY32 lppe);

            [DllImport("kernel32.dll")]
            public static extern IntPtr OpenProcess(Enums.ProcessAccessFlags dwDesiredAccess,
                                                    [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, int dwProcessId);

            [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
            public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize,
                                                       Enums.AllocationType flAllocationType,
                                                       Enums.MemoryProtection flProtect);

            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer,
                                                         int nSize,
                                                         out IntPtr lpNumberOfBytesWritten);

            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer,
                                                         int nSize,
                                                         IntPtr lpNumberOfBytesWritten);

            [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Unicode)]
            public static extern IntPtr LoadLibrary(string lpFileName);

            [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
            public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);
        }
    }
}
