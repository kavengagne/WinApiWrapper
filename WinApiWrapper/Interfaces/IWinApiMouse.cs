using System;
using System.Drawing;

namespace WinApiWrapper.Interfaces
{
    public interface IWinApiMouse
    {
        Point Position { get; set; }
        bool IsVisible { get; set; }
        
        int X { get; set; }
        int Y { get; set; }

        Point GetClientPosition(IntPtr hwnd);
        Point GetClientPosition(IWinApiWindow clientWindow);

        /*
         * Mouse Buttons State
         * 
         * Mouse Buttons Events ?
         * 
         * 
         * 
         */
    }
}