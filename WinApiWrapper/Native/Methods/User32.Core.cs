using System;
using System.Runtime.InteropServices;
using System.Text;


namespace WinApiWrapper.Native.Methods
{
    public partial class User32
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, uint msg, int wParam, StringBuilder lParam);
    }
}
