using System;
using System.Drawing;
using System.Text;
using WinApiWrapper.Interfaces;
using WinApiWrapper.Native.Constants;
using WinApiWrapper.Native.Enums;
using WinApiWrapper.Native.Methods;
using WinApiWrapper.Native.Structs;


namespace WinApiWrapper.Wrappers
{
    public sealed partial class WinApiWindow
    {
        /// <summary>
        /// A handle to the window.
        /// </summary>
        public IntPtr Hwnd { get; }

        /// <summary>
        /// The title of the window.
        /// </summary>
        public string Title
        {
            get
            {
                if (!IsVisible)
                {
                    return null;
                }
                var data = new StringBuilder(4096);
                var messageResult = User32.SendMessage(Hwnd, (uint)WindowMessage.GETTEXT, data.Capacity + 1, data);
                if (messageResult != 0)
                {
                    return data.ToString();
                }
                return null;
            }
            set
            {
                User32.SendMessage(Hwnd, (uint)WindowMessage.SETTEXT, 0, new StringBuilder(value));
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
                if (User32.GetClassName(Hwnd, data, data.Capacity + 1) != 0)
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
            get { return new WinApiWindow(User32.GetParent(Hwnd)); }
            set { User32.SetParent(Hwnd, value.Hwnd); }
        }

        /// <summary>
        ///  The identifier of the thread that created the window.
        /// </summary>
        public IntPtr ProcessId => User32.GetWindowThreadProcessId(Hwnd, IntPtr.Zero);

        public Rectangle Size
        {
            get
            {
                RECT rect;
                if (!User32.GetWindowRect(Hwnd, out rect))
                {
                    return new Rectangle();
                }
                return CreateRectangle(rect);
            }
            set { User32.MoveWindow(Hwnd, value.X, value.Y, value.Width, value.Height, true); }
        }

        public Rectangle ClientSize
        {
            get
            {
                RECT rect;
                if (!User32.GetClientRect(Hwnd, out rect))
                {
                    return new Rectangle();
                }

                return CreateRectangle(rect);
            }
        }

        private static Rectangle CreateRectangle(RECT rect)
        {
            return new Rectangle
            {
                X = rect.Left,
                Y = rect.Top,
                Width = rect.Right - rect.Left,
                Height = rect.Bottom - rect.Top
            };
        }

        /// <summary>
        /// Indicates if the window has the WS_EX_TOPMOST extended style.
        /// </summary>
        public bool IsTopMost
        {
            get
            {
                var windowStyles = User32.GetWindowLong(Hwnd, GetWindowLong.GWL_EXSTYLE);
                return (windowStyles & (uint)WindowStylesEx.WS_EX_TOPMOST) == (uint)WindowStylesEx.WS_EX_TOPMOST;
            }
            set
            {
                var desiredState = value ? WindowOrder.HWND_TOPMOST : WindowOrder.HWND_NOTOPMOST;
                User32.SetWindowPos(Hwnd, desiredState, 0, 0, 0, 0,
                                    SetWindowPosFlags.IgnoreMove | SetWindowPosFlags.IgnoreResize);
            }
        }

        /// <summary>
        /// A boolean value indicating if the window has a parent.
        /// </summary>
        public bool HasParent => Parent.Hwnd != IntPtr.Zero;

        /// <summary>
        /// Indicates if the window is visible
        /// </summary>
        public bool IsVisible
        {
            get { return User32.IsWindowVisible(Hwnd); }
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
        public bool IsDesktopWindow => !HasParent && !IsToolWindow && IsVisible;

        /// <summary>
        /// Indicates if the windows is a tool window.
        /// </summary>
        public bool IsToolWindow
        {
            get
            {
                var windowStyles = User32.GetWindowLong(Hwnd, GetWindowLong.GWL_EXSTYLE);
                return (windowStyles & (uint)WindowStylesEx.WS_EX_TOOLWINDOW) ==
                       (uint)WindowStylesEx.WS_EX_TOOLWINDOW;
            }
            set
            {
                var windowStyles = User32.GetWindowLong(Hwnd, GetWindowLong.GWL_EXSTYLE);
                long newStyle;
                if (value)
                {
                    newStyle = windowStyles | (uint)WindowStylesEx.WS_EX_TOOLWINDOW;
                }
                else
                {
                    newStyle = windowStyles & (uint)~WindowStylesEx.WS_EX_TOOLWINDOW;
                }
                User32.SetWindowLong(Hwnd, GetWindowLong.GWL_EXSTYLE, (int)newStyle);
            }
        }
    }
}
