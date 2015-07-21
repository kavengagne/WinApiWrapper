using System;
using System.Collections.Generic;
using System.Drawing;
using WinApiWrapper.Unsafe;

namespace WinApiWrapper.Interfaces
{
    public interface IWinApiWindow
    {
        // ========== Properties ==========
        IntPtr Hwnd { get; }
        
        string Title { get; set; }
        string ClassName { get; }
        IWinApiWindow Parent { get; set; }
        IntPtr ProcessId { get; }
        Rectangle Size { get; }

        bool IsTopMost { get; set; }
        bool HasParent { get; }
        bool IsVisible { get; set; }
        bool IsDesktopWindow { get; }
        bool IsToolWindow { get; set; }


        //========== Methods ==========
        void Show();
        void Hide();

        void BringToTop();
        void Minimize();
        void Close();

        IntPtr GetProperty(string propertyName);
        bool SetProperty(string propertyName, IntPtr propertyValue);
        void RemoveProperty(string propertyName);
        
        IEnumerable<IWinApiWindow> GetChildWindows(Predicate<IWinApiWindow> condition = null);
    }
}