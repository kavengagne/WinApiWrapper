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
                public MouseButtonAction ButtonAction { get; set; }
                public Action<MouseButton> HookMethod { get; set; }

                public MouseButtonHook(MouseButtonAction buttonAction, Action<MouseButton> hookMethod)
                {
                    ButtonAction = buttonAction;
                    HookMethod = hookMethod;
                }
            }
        }
    }
}