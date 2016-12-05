using System;
using System.Runtime.InteropServices;
using WinApiWrapper.Native.Delegates;
using WinApiWrapper.Native.Enums;
using WinApiWrapper.Native.Methods;


namespace WinApiWrapper
{
    internal class HookHandle : SafeHandle
    {
        internal HookHandle(HookType hookType, HookProc hookProc) : base(IntPtr.Zero, false)
        {
            GCHandle.Alloc(hookProc);
            handle = User32.SetWindowsHookEx(hookType, hookProc, IntPtr.Zero, IntPtr.Zero);
            if (handle == IntPtr.Zero)
            {
                IsInvalid = true;
            }
        }

        protected override bool ReleaseHandle()
        {
            return IsInvalid || User32.UnhookWindowsHookEx(handle);
        }

        public override bool IsInvalid { get; }
    }
}