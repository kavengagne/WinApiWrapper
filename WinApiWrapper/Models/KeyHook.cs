using System.Windows.Forms;
using WinApiWrapper.Enums;


namespace WinApiWrapper.Models
{
    public class KeyHook
    {
        public KeyboardKeyAction Action { get; }
        public Keys Modifiers { get; }
        public Keys Key { get; }

        public KeyHook(KeyboardKeyAction action, Keys key) : this(action, Keys.None, key)
        {
        }

        public KeyHook(KeyboardKeyAction action, Keys modifiers, Keys key)
        {
            Action = action;
            Modifiers = modifiers;
            Key = key;
        }
    }
}