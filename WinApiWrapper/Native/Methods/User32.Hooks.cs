using System;
using System.Runtime.InteropServices;
using WinApiWrapper.Native.Delegates;
using WinApiWrapper.Native.Enums;
using WinApiWrapper.Native.Structs;


namespace WinApiWrapper.Native.Methods
{
    public partial class User32
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(HookType hookType, HookProc lpfn, IntPtr hMod, IntPtr dwThreadId);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);

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

        [DllImport("user32.dll")]
        public static extern IntPtr CallNextHookEx(SafeHandle hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr CallNextHookEx(SafeHandle hhk, int nCode, WindowMessage wParam, [In]KBDLLHOOKSTRUCT lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, WindowMessage wParam, [In]MSLLHOOKSTRUCT lParam);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AddClipboardFormatListener(IntPtr hwnd);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool RemoveClipboardFormatListener(IntPtr hwnd);
    }
}
