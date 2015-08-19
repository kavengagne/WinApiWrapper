using System;
using System.Drawing;
using WinApiWrapper.Enums;


namespace WinApiWrapper.Interfaces
{
    public interface IWinApiMouse
    {
        #region Getters
        Point Position { get; }

        int X { get; }

        int Y { get; }

        bool IsLeftButtonDown { get; }

        bool IsMiddleButtonDown { get; }

        bool IsRightButtonDown { get; }


        Point GetClientPosition(IntPtr hwnd);

        Point GetClientPosition(IWinApiWindow clientWindow); 
        #endregion Getters


        #region Setters
        bool IsVisible { get; set; }

        void SetPosition(Point position);

        void SetX(int x);

        void SetY(int y);

        void Show();

        void Hide();
        #endregion Setters


        #region Actions
        void PerformClick(MouseButton mouseButton, Point position);

        void PerformClick(MouseButton mouseButton, int x, int y);

        void PerformClick(MouseButton mouseButton, Point position, IntPtr hwnd);

        void PerformClick(MouseButton mouseButton, int x, int y, IntPtr hwnd);
        #endregion Actions


        #region Hooks
        // TODO: KG - Test all Hooks properly
        Guid RegisterMoveHook(Action<Point> hookMethod);
        bool UnregisterMoveHook(Guid hookGuid);
        void UnregisterAllMoveHooks();

        Guid RegisterButtonHook(MouseButtonAction buttonAction, Action<MouseButton> hookMethod);
        bool UnregisterButtonHook(Guid hookGuid);
        void UnregisterAllButtonHooks();

        Guid RegisterWheelHook(MouseWheelOrientation wheelOrientation, Action<int> hookMethod);
        bool UnregisterWheelHook(Guid hookGuid);
        void UnregisterAllWheelHooks();

        void UnregisterAllHooks();
        #endregion Hooks
    }
}

