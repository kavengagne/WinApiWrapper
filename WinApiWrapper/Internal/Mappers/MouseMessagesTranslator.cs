using System;
using WinApiWrapper.Enums;
using WinApiWrapper.Native.Enums;


namespace WinApiWrapper
{
    internal partial class Internal {
        internal partial class Mappers
        {
            internal class MouseMessagesTranslator
            {
                private readonly MouseMessagesMappings _mappings;

                public MouseMessagesTranslator()
                {
                    _mappings = new MouseMessagesMappings();
                }

                public MouseButton GetMouseButton(WindowMessage message)
                {
                    MouseButton button;
                    if (!_mappings.WindowMessageToMouseButton.TryGetValue(message, out button))
                    {
                        button = MouseButton.None;
                    }
                    return button;
                }

                public MouseButtonAction GetMouseButtonAction(WindowMessage message)
                {
                    MouseButtonAction action;
                    if (!_mappings.WindowMessageToMouseButtonAction.TryGetValue(message, out action))
                    {
                        action = MouseButtonAction.None;
                    }
                    return action;
                }

                public MouseWheelOrientation GetWheelOrientation(WindowMessage message)
                {
                    MouseWheelOrientation orientation;
                    _mappings.WindowMessageToMouseWheelOrientation.TryGetValue(message, out orientation);
                    return orientation;
                }

                public Tuple<MouseEventF, MouseEventF> GetMouseEventFSequence(MouseButton mouseButton)
                {
                    Tuple<MouseEventF, MouseEventF> sequence;
                    _mappings.MouseButtonToTupleMouseEventF.TryGetValue(mouseButton, out sequence);
                    return sequence;
                }

                public Tuple<WindowMessage, WindowMessage> GetWindowMessageSequence(MouseButton mouseButton)
                {
                    Tuple<WindowMessage, WindowMessage> sequence;
                    _mappings.MouseButtonToTupleWindowMessage.TryGetValue(mouseButton, out sequence);
                    return sequence;
                }
            }
        }
    }
}