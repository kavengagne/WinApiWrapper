using System;
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
                public Dictionary<WindowMessage, KeyboardKeyAction> WindowMessageToKeyboardKeyAction { get; set; }


                public KeyboardMessagesMappings()
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