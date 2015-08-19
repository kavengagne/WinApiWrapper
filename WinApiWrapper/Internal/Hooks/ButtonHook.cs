using System;
using WinApiWrapper.Enums;


namespace WinApiWrapper.Internal.Hooks
{
    internal class ButtonHook
    {
        public MouseButtonAction ButtonAction { get; set; }
        public Action<MouseButton> HookMethod { get; set; }

        public ButtonHook(MouseButtonAction buttonAction, Action<MouseButton> hookMethod)
        {
            ButtonAction = buttonAction;
            HookMethod = hookMethod;
        }
    }
}