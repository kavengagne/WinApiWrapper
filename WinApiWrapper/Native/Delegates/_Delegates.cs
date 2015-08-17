using System;

// ReSharper disable InconsistentNaming


namespace WinApiWrapper.Native.Delegates
{
    public delegate IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);


    public delegate int NtCreateThreadExInvoker(
        ref IntPtr ThreadHandle, Enums.AccessMask DesiredAccess, IntPtr ObjectAttributes, IntPtr ProcessHandle,
        IntPtr lpStartAddress, IntPtr lpParameter, bool CreateSuspended = false, int dwStackSize = 0, int Unknown1 = 0,
        int Unknown2 = 0, int Unknown3 = 0);


    public delegate IntPtr HookProc(int code, IntPtr wParam, IntPtr lParam);


    public delegate void WinEventProc(
        IntPtr winEventHookHandle, Enums.AccessibleEvents accEvent, IntPtr windowHandle, int objectId, int childId,
        uint eventThreadId, uint eventTimeInMilliseconds);


    public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
}
