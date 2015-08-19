using System;
using System.Collections.Generic;
using WinApiWrapper.Enums;
using WinApiWrapper.Native.Enums;


namespace WinApiWrapper.Internal.Mappers
{
    internal class MouseMessagesMappings
    {
        public MouseMessagesMappings()
        {
            WindowMessageToMouseButton = new Dictionary<WindowMessage, MouseButton>
            {
                { WindowMessage.LBUTTONDOWN, MouseButton.Left },
                { WindowMessage.LBUTTONUP, MouseButton.Left },
                { WindowMessage.LBUTTONDBLCLK, MouseButton.Left },
                { WindowMessage.RBUTTONDOWN, MouseButton.Right },
                { WindowMessage.RBUTTONUP, MouseButton.Right },
                { WindowMessage.RBUTTONDBLCLK, MouseButton.Right },
                { WindowMessage.MBUTTONDOWN, MouseButton.Middle },
                { WindowMessage.MBUTTONUP, MouseButton.Middle },
                { WindowMessage.MBUTTONDBLCLK, MouseButton.Middle }
            };

            WindowMessageToMouseButtonAction = new Dictionary<WindowMessage, MouseButtonAction>
            {
                { WindowMessage.LBUTTONUP, MouseButtonAction.Up },
                { WindowMessage.RBUTTONUP, MouseButtonAction.Up },
                { WindowMessage.MBUTTONUP, MouseButtonAction.Up },
                { WindowMessage.LBUTTONDOWN, MouseButtonAction.Down },
                { WindowMessage.RBUTTONDOWN, MouseButtonAction.Down },
                { WindowMessage.MBUTTONDOWN, MouseButtonAction.Down },
                { WindowMessage.LBUTTONDBLCLK, MouseButtonAction.DoubleClick },
                { WindowMessage.RBUTTONDBLCLK, MouseButtonAction.DoubleClick },
                { WindowMessage.MBUTTONDBLCLK, MouseButtonAction.DoubleClick },
            };

            MouseButtonToTupleMouseEventF = new Dictionary<MouseButton, Tuple<MouseEventF, MouseEventF>>
            {
                { MouseButton.Left, Tuple.Create(MouseEventF.LEFTDOWN, MouseEventF.LEFTUP) },
                { MouseButton.Right, Tuple.Create(MouseEventF.RIGHTDOWN, MouseEventF.RIGHTUP) },
                { MouseButton.Middle, Tuple.Create(MouseEventF.MIDDLEDOWN, MouseEventF.MIDDLEUP) }
            };

            MouseButtonToTupleWindowMessage = new Dictionary<MouseButton, Tuple<WindowMessage, WindowMessage>>
            {
                { MouseButton.Left, Tuple.Create(WindowMessage.LBUTTONDOWN, WindowMessage.LBUTTONUP) },
                { MouseButton.Right, Tuple.Create(WindowMessage.RBUTTONDOWN, WindowMessage.RBUTTONUP) },
                { MouseButton.Middle, Tuple.Create(WindowMessage.MBUTTONDOWN, WindowMessage.MBUTTONUP) }
            };

            WindowMessageToMouseWheelOrientation = new Dictionary<WindowMessage, MouseWheelOrientation>
            {
                { WindowMessage.MOUSEWHEEL, MouseWheelOrientation.Vertical },
                { WindowMessage.MOUSEHWHEEL, MouseWheelOrientation.Horizontal }
            };
        }

        public Dictionary<MouseButton, Tuple<WindowMessage, WindowMessage>> MouseButtonToTupleWindowMessage { get; set; }

        public Dictionary<MouseButton, Tuple<MouseEventF, MouseEventF>> MouseButtonToTupleMouseEventF { get; set; }

        public Dictionary<WindowMessage, MouseButtonAction> WindowMessageToMouseButtonAction { get; set; }

        public Dictionary<WindowMessage, MouseButton> WindowMessageToMouseButton { get; set; }
        public Dictionary<WindowMessage, MouseWheelOrientation> WindowMessageToMouseWheelOrientation { get; set; }
    }
}