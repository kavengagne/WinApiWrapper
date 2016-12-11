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

        public override bool Equals(object obj)
        {
            var other = obj as WinApiWindow;
            if (other != null)
            {
                return other.Hwnd == Hwnd;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Hwnd.GetHashCode();
        }
    }
}
