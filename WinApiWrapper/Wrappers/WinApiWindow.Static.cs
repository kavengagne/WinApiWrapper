using System;
using System.Collections.Generic;
using System.Diagnostics;
using WinApiWrapper.Interfaces;
using WinApiWrapper.Unsafe;

namespace WinApiWrapper.Wrappers
{
    public partial class WinApiWindow
    {
        public static IEnumerable<IWinApiWindow> EnumWindows(Predicate<IWinApiWindow> condition = null)
        {
            var windowsList = new List<IWinApiWindow>();
            NativeMethods.User32.EnumWindowsProc windowEnumDelegate = (wnd, param) =>
            {
                var window = new WinApiWindow(wnd);
                if (condition == null || condition.Invoke(window))
                {
                    windowsList.Add(window);
                }
                return true;
            };
            NativeMethods.User32.EnumWindows(windowEnumDelegate, IntPtr.Zero);
            return windowsList;
        }

        public static IEnumerable<IWinApiWindow> EnumChildWindows(IntPtr hWnd, Predicate<IWinApiWindow> condition = null)
        {
            var windowsList = new List<IWinApiWindow>();
            NativeMethods.User32.EnumWindowsProc windowEnumDelegate = (wnd, param) =>
            {
                var window = new WinApiWindow(wnd);
                if (condition == null || condition.Invoke(window))
                {
                    windowsList.Add(window);
                }
                return true;
            };
            NativeMethods.User32.EnumChildWindows(hWnd, windowEnumDelegate, IntPtr.Zero);
            return windowsList;
        }

        public static IWinApiWindow CreateWindow()
        {
            var hwnd = NativeMethods.User32.CreateWindowEx(NativeMethods.Enums.WindowStylesEx.WS_EX_OVERLAPPEDWINDOW,
                                                           "KAVEN_WINDOW", "KAVEN WINDOW",
                                                           NativeMethods.Enums.WindowStyles.WS_OVERLAPPEDWINDOW, 0, 0,
                                                           200, 200,
                                                           IntPtr.Zero, IntPtr.Zero, Process.GetCurrentProcess().Handle,
                                                           IntPtr.Zero);

            NativeMethods.User32.ShowWindow(hwnd, NativeMethods.Enums.ShowWindowCommands.Show);

            // TODO: KG - Implement correct window creation
            return new WinApiWindow(IntPtr.Zero);
        }

        public static IWinApiWindow GetDesktopWindow()
        {
            return new WinApiWindow(NativeMethods.User32.GetDesktopWindow());
        }

        public static IWinApiWindow GetForegroundWindow()
        {
            return new WinApiWindow(NativeMethods.User32.GetForegroundWindow());
        }
    }
}
