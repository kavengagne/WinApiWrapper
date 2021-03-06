using System;
using System.Windows.Forms;
using WinApiWrapper.Enums;
using WinApiWrapper.Models;
using WinApiWrapper.Wrappers;


namespace WinApiWrapper
{
    internal partial class Internal
    {
        internal partial class Hooks
        {
            internal class KeyboardKeyHook : KeyboardHookBase
            {
                internal KeyHook KeyHook { get; set; }

                internal KeyboardKeyHook(KeyHook keyHook, Action hookMethod) : base(hookMethod)
                {
                    KeyHook = keyHook;
                }

                protected override bool CanInvoke(KeyboardKeyAction action, Keys modifiers, Keys key)
                {
                    return !WinApiKeyboard.IsModifier(key) && KeyHook.CanTrigger(action, modifiers, key);
                }
            }
        }
    }
}