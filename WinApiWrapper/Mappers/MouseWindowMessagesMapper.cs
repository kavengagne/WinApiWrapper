using System;
using System.Collections.Generic;
using WinApiWrapper.Enums;
using WinApiWrapper.Native.Enums;


namespace WinApiWrapper.Mappers
{
    public class MouseWindowMessagesMapper :
        MapperBase<WinApiMouseButton, Tuple<WindowMessages, WindowMessages>>
    {
        public MouseWindowMessagesMapper()
        {
            Mappings = new Dictionary
                <WinApiMouseButton, Tuple<WindowMessages, WindowMessages>>
            {
                {
                    WinApiMouseButton.Left,
                    Tuple.Create(WindowMessages.LBUTTONDOWN, WindowMessages.LBUTTONUP)
                },
                {
                    WinApiMouseButton.Right,
                    Tuple.Create(WindowMessages.RBUTTONDOWN, WindowMessages.RBUTTONUP)
                }
            };
        }
    }
}
