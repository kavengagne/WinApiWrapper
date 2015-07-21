using System;
using System.Collections.Generic;
using WinApiWrapper.Interfaces;
using WinApiWrapper.Unsafe;

namespace WinApiWrapper.Wrappers
{
    public partial class WinApiWindow
    {
        /// <summary>
        /// Shows the window.
        /// </summary>
        public void Show()
        {
            NativeMethods.User32.ShowWindow(Hwnd, NativeMethods.Enums.ShowWindowCommands.Show);
        }

        /// <summary>
        /// Hides the window.
        /// </summary>
        public void Hide()
        {
            NativeMethods.User32.ShowWindow(Hwnd, NativeMethods.Enums.ShowWindowCommands.Hide);
        }

        /// <summary>
        /// Brings the specified window to the top of the Z order. If the window is a top-level window, it is activated.
        /// If the window is a child window, the top-level parent window associated with the child window is activated.
        /// </summary>
        public void BringToTop()
        {
            NativeMethods.User32.BringWindowToTop(Hwnd);
        }

        /// <summary>
        /// Minimizes (but does not destroy) the specified window.
        /// </summary>
        public void Minimize()
        {
            NativeMethods.User32.CloseWindow(Hwnd);
        }

        /// <summary>
        /// <para>The DestroyWindow function destroys the specified window. The function sends WM_DESTROY and WM_NCDESTROY messages to the window to deactivate it and remove the keyboard focus from it. The function also destroys the window's menu, flushes the thread message queue, destroys timers, removes clipboard ownership, and breaks the clipboard viewer chain (if the window is at the top of the viewer chain).</para>
        /// <para>If the specified window is a parent or owner window, DestroyWindow automatically destroys the associated child or owned windows when it destroys the parent or owner window. The function first destroys child or owned windows, and then it destroys the parent or owner window.</para>
        /// </summary>
        public void Close()
        {
            NativeMethods.User32.DestroyWindow(Hwnd);
        }

        /// <summary>
        /// Retrieves a data handle from the property list of the window.
        /// Returns null if the property does not exist.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns></returns>
        public IntPtr GetProperty(string propertyName)
        {
            return NativeMethods.User32.GetProp(Hwnd, propertyName);
        }

        /// <summary>
        /// Adds a new entry or changes an existing entry in the property list of the specified window.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="propertyValue">A handle to the data to be associated with this property.</param>
        /// <returns></returns>
        public bool SetProperty(string propertyName, IntPtr propertyValue)
        {
            return NativeMethods.User32.SetProp(Hwnd, propertyName, propertyValue);
        }

        /// <summary>
        /// Removes an entry from the property list of the window.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        public void RemoveProperty(string propertyName)
        {
            NativeMethods.User32.RemoveProp(Hwnd, propertyName);
        }

        public IEnumerable<IWinApiWindow> GetChildWindows(Predicate<IWinApiWindow> condition = null)
        {
            return EnumChildWindows(Hwnd, condition);
        }
    }
}