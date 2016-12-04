using System;
using System.Windows.Forms;
using WinApiWrapper.Enums;


namespace WinApiWrapper
{
    internal partial class Internal
    {
        internal partial class Hooks
        {
            internal class KeyboardKeyHook
            {
                public KeyboardKeyAction KeyAction { get; set; }
                public Action<Keys> HookMethod { get; set; }

                public KeyboardKeyHook(KeyboardKeyAction keyAction, Action<Keys> hookMethod)
                {
                    KeyAction = keyAction;
                    HookMethod = hookMethod;
                }
            }
        }
    }
}