using System.Collections.Generic;
using WinApiWrapper.Enums;
using WinApiWrapper.Native.Enums;


namespace WinApiWrapper
{
    internal partial class Internal
    {
        internal partial class Mappers
        {
            internal class KeyboardMessagesMappings
            {
                internal Dictionary<WindowMessage, KeyboardKeyAction> WindowMessageToKeyboardKeyAction { get; set; }

                internal KeyboardMessagesMappings()
                {
                    WindowMessageToKeyboardKeyAction = new Dictionary<WindowMessage, KeyboardKeyAction>
                    {
                        { WindowMessage.KEYDOWN, KeyboardKeyAction.Down },
                        { WindowMessage.KEYUP, KeyboardKeyAction.Up }
                    };
                }
            }
        }
    }
}