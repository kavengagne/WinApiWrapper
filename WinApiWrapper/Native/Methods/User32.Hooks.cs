using System;
using System.Runtime.InteropServices;
using WinApiWrapper.Native.Delegates;


namespace WinApiWrapper.Native.Methods
{
    public partial class User32
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(Enums.HookType hookType, HookProc lpfn, IntPtr hMod,
                                                     IntPtr dwThreadId);

        [DllImport("user32", EntryPoint = "CallNextHookEx")]
        public static extern IntPtr CallNextHook(IntPtr hHook, int ncode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr SetWinEventHook(Enums.AccessibleEvents eventMin, Enums.AccessibleEvents eventMax,
                                                    IntPtr eventHookAssemblyHandle,
                                                    WinEventProc eventHookHandle, uint processId,
                                                    uint threadId, Enums.SetWinEventHookParameter parameterFlags);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll")]
        public static extern bool UnhookWinEvent(IntPtr eventHookHandle);
    }
}
