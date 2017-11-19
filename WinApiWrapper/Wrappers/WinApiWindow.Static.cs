using System;
using System.Collections.Generic;
using System.Diagnostics;
using WinApiWrapper.Interfaces;
using WinApiWrapper.Native.Delegates;
using WinApiWrapper.Native.Enums;
using WinApiWrapper.Native.Methods;


namespace WinApiWrapper.Wrappers
{
    public partial class WinApiWindow : IWinApiWindow
    {
        public static IEnumerable<IWinApiWindow> EnumWindows(Predicate<IWinApiWindow> condition = null)
        {
            var windowsList = new List<IWinApiWindow>();
            EnumWindowsProc windowEnumDelegate = (wnd, param) =>
            {
                var window = new WinApiWindow(wnd);
                if (condition == null || condition.Invoke(window))
                {
                    windowsList.Add(window);
                }
                return true;
            };
            User32.EnumWindows(windowEnumDelegate, IntPtr.Zero);
            return windowsList;
        }

        public static IEnumerable<IWinApiWindow> EnumChildWindows(IntPtr hWnd, Predicate<IWinApiWindow> condition = null)
        {
            var windowsList = new List<IWinApiWindow>();
            EnumWindowsProc windowEnumDelegate = (wnd, param) =>
            {
                var window = new WinApiWindow(wnd);
                if (condition == null || condition.Invoke(window))
                {
                    windowsList.Add(window);
                }
                return true;
            };
            User32.EnumChildWindows(hWnd, windowEnumDelegate, IntPtr.Zero);
            return windowsList;
        }

        public static IWinApiWindow CreateWindow()
        {
            var hwnd = User32.CreateWindowEx(WindowStylesEx.WS_EX_OVERLAPPEDWINDOW,
                                             "KAVEN_WINDOW", "KAVEN WINDOW",
                                             WindowStyles.WS_OVERLAPPEDWINDOW, 0, 0,
                                             200, 200,
                                             IntPtr.Zero, IntPtr.Zero, Process.GetCurrentProcess().Handle,
                                             IntPtr.Zero);

            User32.ShowWindow(hwnd, ShowWindowCommands.Show);

            // TODO: KG - Implement correct window creation
            return new WinApiWindow(IntPtr.Zero);
        }

        public static IWinApiWindow GetDesktopWindow()
        {
            return new WinApiWindow(User32.GetDesktopWindow());
        }

        public static IWinApiWindow GetForegroundWindow()
        {
            return new WinApiWindow(User32.GetForegroundWindow());
        }

        public static IWinApiWindow FindByName(string title)
        {
            return Find(null, title);
        }

        public static IWinApiWindow FindByClassName(string className)
        {
            return Find(className, null);
        }

        public static IWinApiWindow Find(string classNameOrNull, string titleOrNull)
        {
            var hwnd = User32.FindWindow(classNameOrNull, titleOrNull);
            if (hwnd != IntPtr.Zero)
            {
                return new WinApiWindow(hwnd);
            }
            return null;
        }
    }
}
