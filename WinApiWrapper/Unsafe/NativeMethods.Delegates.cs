using System;

// ReSharper disable InconsistentNaming

namespace WinApiWrapper.Unsafe
{
    public static partial class NativeMethods
    {
        public class Delegates
        {
            public delegate IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

            public delegate int NtCreateThreadExInvoker(ref IntPtr ThreadHandle, Enums.ACCESS_MASK DesiredAccess,
                                                        IntPtr ObjectAttributes, IntPtr ProcessHandle,
                                                        IntPtr lpStartAddress,
                                                        IntPtr lpParameter, bool CreateSuspended = false,
                                                        int dwStackSize = 0, int Unknown1 = 0, int Unknown2 = 0,
                                                        int Unknown3 = 0);
        }
    }
}
