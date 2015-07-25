using System;
using System.Drawing;
using WinApiWrapper.Enums;

namespace WinApiWrapper.Interfaces
{
    public interface IWinApiMouse
    {
        // Getters
        Point Position { get; }
        int X { get; }
        int Y { get; }
        bool IsLeftButtonDown { get; }
        bool IsRightButtonDown { get; }
        Point GetClientPosition(IntPtr hwnd);
        Point GetClientPosition(IWinApiWindow clientWindow);

        // Setters
        bool IsVisible { get; set; }
        void SetPosition(Point position);
        void SetX(int x);
        void SetY(int y);
        void Show();
        void Hide();
        
        // Actions
        void PerformClick(WinApiMouseButton mouseButton, Point position);
        void PerformClick(WinApiMouseButton mouseButton, int x, int y);
        void PerformClick(WinApiMouseButton mouseButton, IntPtr hwnd, Point position);
        void PerformClick(WinApiMouseButton mouseButton, IntPtr hwnd, int x, int y);
    }
}
