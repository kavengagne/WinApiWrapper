using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WinApiWrapper.Enums;
using WinApiWrapper.Native.Enums;
using WinApiWrapper.Native.Structs;


namespace WinApiWrapper
{
    internal partial class Internal {
        internal partial class Mappers
        {
            internal class KeyboardMessagesTranslator
            {
                private readonly KeyboardMessagesMappings _mappings;

                public KeyboardMessagesTranslator()
                {
                    _mappings = new KeyboardMessagesMappings();
                }

                public KeyboardKeyAction GetKeyboardKeyAction(WindowMessage message)
                {
                    KeyboardKeyAction action;
                    if (!_mappings.WindowMessageToKeyboardKeyAction.TryGetValue(message, out action))
                    {
                        action = KeyboardKeyAction.None;
                    }
                    return action;
                }

                public Keys GetKeyboardKey(IntPtr lparam)
                {
                    var kbHookStruct = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lparam, typeof(KBDLLHOOKSTRUCT));
                    Keys key;
                    if (Enum.TryParse(kbHookStruct.vkCode.ToString(), out key))
                    {
                        return key;
                    }
                    return Keys.None;
                }
            }
        }
    }
}