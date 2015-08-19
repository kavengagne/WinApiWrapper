using System;
using WinApiWrapper.Enums;


namespace WinApiWrapper.Internal.Hooks
{
    internal class WheelHook
    {
        public MouseWheelOrientation WheelOrientation { get; set; }
        public Action<int> HookMethod { get; set; }

        public WheelHook(MouseWheelOrientation wheelOrientation, Action<int> hookMethod)
        {
            WheelOrientation = wheelOrientation;
            HookMethod = hookMethod;
        }
    }
}
