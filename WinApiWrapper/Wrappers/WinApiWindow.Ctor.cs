using System;
using WinApiWrapper.Interfaces;


namespace WinApiWrapper.Wrappers
{
    public partial class WinApiWindow : IWinApiWindow
    {
        public WinApiWindow(IntPtr hwnd)
        {
            Hwnd = hwnd;
        }
    }
}
