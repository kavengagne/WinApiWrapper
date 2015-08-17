using System;
using System.Runtime.InteropServices;


namespace WinApiWrapper.Native.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct CURSORINFO
    {
        public Int32 cbSize; // Specifies the size, in bytes, of the structure. 
        // The caller must set this to Marshal.SizeOf(typeof(CURSORINFO)).
        public Int32 flags; // Specifies the cursor state. This parameter can be one of the following values:
        //    0             The cursor is hidden.
        //    CURSOR_SHOWING    The cursor is showing.
        public IntPtr hCursor; // Handle to the cursor. 
        public POINT ptScreenPos; // A POINT structure that receives the screen coordinates of the cursor. 
    }
}