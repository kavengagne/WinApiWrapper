using System;
using WinApiWrapper.Enums;


namespace WinApiWrapper
{
    internal partial class Internal
    {
        internal partial class Hooks
        {
            internal class MouseButtonHook
            {
                internal MouseButtonAction ButtonAction { get; set; }
                internal Action<MouseButton> HookMethod { get; set; }

                internal MouseButtonHook(MouseButtonAction buttonAction, Action<MouseButton> hookMethod)
                {
                    ButtonAction = buttonAction;
                    HookMethod = hookMethod;
                }
            }
        }
    }
}