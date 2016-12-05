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
                internal MouseWheelOrientation WheelOrientation { get; set; }
                internal Action<int> HookMethod { get; set; }

                internal WheelHook(MouseWheelOrientation wheelOrientation, Action<int> hookMethod)
                {
                    WheelOrientation = wheelOrientation;
                    HookMethod = hookMethod;
                }
            }
        }
    }
}
