using System;
using WinApiWrapper.Enums;


namespace WinApiWrapper
{
    internal partial class Internal
    {
        internal partial class Hooks
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
    }
}
