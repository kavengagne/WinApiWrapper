using System;


namespace WinApiWrapper.Native.Structs
{
    public struct NTSTATUS
    {
        public IntPtr hThread;
        public Enums.AccessMask DesiredAccess;
        public IntPtr ObjectAttributes;
        public IntPtr ProcessHandle;
        public IntPtr lpStartAddress;
        public IntPtr lpParameter;
        public bool CreateSuspended;
        public int StackZeroBits;
        public int SizeOfStackCommit;
        public int SizeOfStackReserve;
        public IntPtr lpBytesBuffer;
    }
}