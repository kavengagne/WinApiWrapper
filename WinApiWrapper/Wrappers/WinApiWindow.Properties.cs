using System;
using System.Drawing;
using System.Text;
using WinApiWrapper.Interfaces;
using WinApiWrapper.Unsafe;

namespace WinApiWrapper.Wrappers
{
    public sealed partial class WinApiWindow
    {
        /// <summary>
        /// A handle to the window.
        /// </summary>
        public IntPtr Hwnd { get; private set; }

        /// <summary>
        /// The title of the window.
        /// </summary>
        public string Title
        {
            get
            {
                var data = new StringBuilder(4096);
                var messageResult = NativeMethods.User32.SendMessage(Hwnd, NativeMethods.Constants.WM_GETTEXT,
                                                                     data.Capacity + 1, data);
                if (messageResult != 0)
                {
                    return data.ToString();
                }
                return null;
            }
            set
            {
                NativeMethods.User32.SendMessage(Hwnd, NativeMethods.Constants.WM_SETTEXT, 0, new StringBuilder(value));
            }
        }

        /// <summary>
        /// The name of the class to which the specified window belongs.
        /// </summary>
        public string ClassName
        {
            get
            {
                var data = new StringBuilder(4096);
                if (NativeMethods.User32.GetClassName(Hwnd, data, data.Capacity + 1) != 0)
                {
                    return data.ToString();
                }
                return null;
            }
        }

        /// <summary>
        /// An instance of the window's parent or owner. Returns null if the window has no parent.
        /// </summary>
        public IWinApiWindow Parent
        {
            get { return new WinApiWindow(NativeMethods.User32.GetParent(Hwnd)); }
            set { NativeMethods.User32.SetParent(Hwnd, value.Hwnd); }
        }

        /// <summary>
        ///  The identifier of the thread that created the window.
        /// </summary>
        public IntPtr ProcessId
        {
            get { return NativeMethods.User32.GetWindowThreadProcessId(Hwnd, IntPtr.Zero); }
        }

        public Rectangle Size
        {
            get
            {
                NativeMethods.Structs.RECT rect;
                if (!NativeMethods.User32.GetWindowRect(Hwnd, out rect))
                {
                    return new Rectangle();
                }
                return new Rectangle
                {
                    X = rect.Left,
                    Y = rect.Top,
                    Width = rect.Right - rect.Left + 1,
                    Height = rect.Bottom - rect.Top + 1
                };
            }
        }

        /// <summary>
        /// Indicates if the window has the WS_EX_TOPMOST extended style.
        /// </summary>
        public bool IsTopMost
        {
            get
            {
                var windowStyles = NativeMethods.User32.GetWindowLong(Hwnd, NativeMethods.Enums.GWL.GWL_EXSTYLE);
                return (windowStyles & NativeMethods.Constants.WS_EX_TOPMOST) == NativeMethods.Constants.WS_EX_TOPMOST;
            }
            set
            {
                var desiredState = value ? NativeMethods.Constants.HWND_TOPMOST : NativeMethods.Constants.HWND_NOTOPMOST;
                NativeMethods.User32.SetWindowPos(
                    Hwnd, desiredState, 0, 0, 0, 0,
                    NativeMethods.Enums.SetWindowPosFlags.IgnoreMove |
                    NativeMethods.Enums.SetWindowPosFlags.IgnoreResize);
            }
        }

        /// <summary>
        /// A boolean value indicating if the window has a parent.
        /// </summary>
        public bool HasParent
        {
            get { return Parent.Hwnd != IntPtr.Zero; }
        }

        /// <summary>
        /// Indicates if the window is visible
        /// </summary>
        public bool IsVisible
        {
            get { return NativeMethods.User32.IsWindowVisible(Hwnd); }
            set
            {
                if (value)
                {
                    Show();
                }
                else
                {
                    Hide();
                }
            }
        }

        /// <summary>
        /// Indicates if the window is a top-level desktop window.
        /// </summary>
        public bool IsDesktopWindow
        {
            get { return !HasParent && !IsToolWindow && IsVisible; }
        }

        /// <summary>
        /// Indicates if the windows is a tool window.
        /// </summary>
        public bool IsToolWindow
        {
            get
            {
                var windowStyles = NativeMethods.User32.GetWindowLong(Hwnd, NativeMethods.Enums.GWL.GWL_EXSTYLE);
                return (windowStyles & NativeMethods.Constants.WS_EX_TOOLWINDOW) ==
                       NativeMethods.Constants.WS_EX_TOOLWINDOW;
            }
            set
            {
                var windowStyles = NativeMethods.User32.GetWindowLong(Hwnd, NativeMethods.Enums.GWL.GWL_EXSTYLE);
                long newStyle;
                if (value)
                {
                    newStyle = windowStyles | NativeMethods.Constants.WS_EX_TOOLWINDOW;
                }
                else
                {
                    newStyle = windowStyles & ~NativeMethods.Constants.WS_EX_TOOLWINDOW;
                }
                NativeMethods.User32.SetWindowLong(Hwnd, NativeMethods.Enums.GWL.GWL_EXSTYLE, (int)newStyle);
            }
        }
    }
}
