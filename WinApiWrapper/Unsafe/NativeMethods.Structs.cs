using System;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;

// ReSharper disable InconsistentNaming

namespace WinApiWrapper.Unsafe
{
    public static partial class NativeMethods
    {
        public class Structs
        {
            [StructLayout(LayoutKind.Sequential)]
            public struct WNDCLASS
            {
                public Enums.ClassStyles style;
                [MarshalAs(UnmanagedType.FunctionPtr)]
                public Delegates.WndProc lpfnWndProc;
                public int cbClsExtra;
                public int cbWndExtra;
                public IntPtr hInstance;
                public IntPtr hIcon;
                public IntPtr hCursor;
                public IntPtr hbrBackground;
                [MarshalAs(UnmanagedType.LPTStr)]
                public string lpszMenuName;
                [MarshalAs(UnmanagedType.LPTStr)]
                public string lpszClassName;
            }

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
            public struct WNDCLASSEX
            {
                [MarshalAs(UnmanagedType.U4)]
                public int cbSize;

                [MarshalAs(UnmanagedType.U4)]
                public int style;

                public IntPtr lpfnWndProc; // not WndProc
                public int cbClsExtra;
                public int cbWndExtra;
                public IntPtr hInstance;
                public IntPtr hIcon;
                public IntPtr hCursor;
                public IntPtr hbrBackground;
                public string lpszMenuName;
                public string lpszClassName;
                public IntPtr hIconSm;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct PROCESSENTRY32
            {
                public uint dwSize;
                public int cntUsage;
                public int th32ProcessID;
                public IntPtr th32DefaultHeapID;
                public int th32ModuleID;
                public int cntThreads;
                public int th32ParentProcessID;
                public int pcPriClassBase;
                public int dwFlags;

                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
                public string szExeFile;
            };

            public struct TOKEN_PRIVILEGES
            {
                public uint PrivilegeCount;

                [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.ANYSIZE_ARRAY)]
                public LUID_AND_ATTRIBUTES[] Privileges;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct LUID_AND_ATTRIBUTES
            {
                public LUID Luid;
                public uint Attributes;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct LUID
            {
                public uint LowPart;
                public int HighPart;
            }

            public struct NTSTATUS
            {
                public IntPtr hThread;
                public Enums.ACCESS_MASK DesiredAccess;
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

            [StructLayout(LayoutKind.Sequential)]
            public struct RECT
            {
                public int Left, Top, Right, Bottom;

                public RECT(int left, int top, int right, int bottom)
                {
                    Left = left;
                    Top = top;
                    Right = right;
                    Bottom = bottom;
                }

                public RECT(Rectangle r) : this(r.Left, r.Top, r.Right, r.Bottom)
                {
                }

                public int X
                {
                    get { return Left; }
                    set { Right -= (Left - value); Left = value; }
                }

                public int Y
                {
                    get { return Top; }
                    set { Bottom -= (Top - value); Top = value; }
                }

                public int Height
                {
                    get { return Bottom - Top; }
                    set { Bottom = value + Top; }
                }

                public int Width
                {
                    get { return Right - Left; }
                    set { Right = value + Left; }
                }

                public Point Location
                {
                    get { return new Point(Left, Top); }
                    set { X = value.X; Y = value.Y; }
                }

                public Size Size
                {
                    get { return new Size(Width, Height); }
                    set { Width = value.Width; Height = value.Height; }
                }

                public static implicit operator Rectangle(RECT r)
                {
                    return new Rectangle(r.Left, r.Top, r.Width, r.Height);
                }

                public static implicit operator RECT(Rectangle r)
                {
                    return new RECT(r);
                }

                public static bool operator ==(RECT r1, RECT r2)
                {
                    return r1.Equals(r2);
                }

                public static bool operator !=(RECT r1, RECT r2)
                {
                    return !r1.Equals(r2);
                }

                public bool Equals(RECT r)
                {
                    return r.Left == Left && r.Top == Top && r.Right == Right && r.Bottom == Bottom;
                }

                public override bool Equals(object obj)
                {
                    if (obj is RECT)
                    {
                        return Equals((RECT)obj);
                    }
                    if (obj is Rectangle)
                    {
                        return Equals(new RECT((Rectangle)obj));
                    }
                    return false;
                }

                public override int GetHashCode()
                {
                    return ((Rectangle)this).GetHashCode();
                }

                public override string ToString()
                {
                    return String.Format(CultureInfo.CurrentCulture, "{{Left={0},Top={1},Right={2},Bottom={3}}}", Left, Top, Right, Bottom);
                }
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct POINT
            {
                public Int32 x;
                public Int32 y;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct CURSORINFO
            {
                public Int32 cbSize;        // Specifies the size, in bytes, of the structure. 
                // The caller must set this to Marshal.SizeOf(typeof(CURSORINFO)).
                public Int32 flags;         // Specifies the cursor state. This parameter can be one of the following values:
                //    0             The cursor is hidden.
                //    CURSOR_SHOWING    The cursor is showing.
                public IntPtr hCursor;          // Handle to the cursor. 
                public POINT ptScreenPos;       // A POINT structure that receives the screen coordinates of the cursor. 
            }
        }
    }
}
