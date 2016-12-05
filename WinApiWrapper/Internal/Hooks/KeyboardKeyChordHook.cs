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
            internal class KeyboardKeyChordHook : KeyboardHookBase
            {
                internal KeyChord KeyChord { get; set; }

                internal KeyboardKeyChordHook(KeyChord keyChord, Action hookMethod) : base(hookMethod)
                {
                    KeyChord = keyChord;
                }

                protected override bool CanInvoke(KeyboardKeyAction action, Keys modifiers, Keys key)
                {
                    return !WinApiKeyboard.IsModifier(key) && KeyChord.TrySatisfy(action, modifiers, key);
                }
            }
        }
    }
}