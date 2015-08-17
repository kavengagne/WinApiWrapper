using System;
using System.Collections.Generic;
using WinApiWrapper.Enums;
using WinApiWrapper.Native.Enums;


namespace WinApiWrapper.Mappers
{
    public class MouseEventMessagesMapper :
        MapperBase<WinApiMouseButton, Tuple<MouseEventF, MouseEventF>>
    {
        public MouseEventMessagesMapper()
        {
            Mappings = new Dictionary
                <WinApiMouseButton, Tuple<MouseEventF, MouseEventF>>
            {
                {
                    WinApiMouseButton.Left,
                    Tuple.Create(MouseEventF.LEFTDOWN, MouseEventF.LEFTUP)
                },
                {
                    WinApiMouseButton.Middle,
                    Tuple.Create(MouseEventF.MIDDLEDOWN, MouseEventF.MIDDLEUP)
                },
                {
                    WinApiMouseButton.Right,
                    Tuple.Create(MouseEventF.RIGHTDOWN, MouseEventF.RIGHTUP)
                }
            };
        }
    }
}
