using System.Windows.Forms;
using WinApiWrapper.Enums;


namespace WinApiWrapper.Models
{
    public class KeyHook
    {
        internal KeyboardKeyAction Action { get; }
        internal Keys Modifiers { get; }
        internal Keys Key { get; }

        public KeyHook(KeyboardKeyAction action, Keys key) : this(action, Keys.None, key)
        {
        }

        public KeyHook(KeyboardKeyAction action, Keys modifiers, Keys key)
        {
            Action = action;
            Modifiers = modifiers;
            Key = key;
        }

        internal bool CanTrigger(KeyboardKeyAction action, Keys modifiers, Keys key)
        {
            return Action == action && Key == key && HasRequiredModifiers(modifiers);
        }

        private bool HasRequiredModifiers(Keys currentModifiers)
        {
            return Modifiers == Keys.None || currentModifiers.HasFlag(Modifiers);
        }
    }
}