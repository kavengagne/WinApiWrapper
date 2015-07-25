using System;
using System.Collections.Generic;
using WinApiWrapper.Enums;
using WinApiWrapper.Unsafe;

namespace WinApiWrapper.Mappers
{
    public class MouseEventMessagesMapper :
        MapperBase<WinApiMouseButton, Tuple<NativeMethods.Enums.MOUSEEVENTF, NativeMethods.Enums.MOUSEEVENTF>>
    {
        public MouseEventMessagesMapper()
        {
            Mappings = new Dictionary
                <WinApiMouseButton, Tuple<NativeMethods.Enums.MOUSEEVENTF, NativeMethods.Enums.MOUSEEVENTF>>
            {
                {
                    WinApiMouseButton.Left,
                    Tuple.Create(NativeMethods.Enums.MOUSEEVENTF.LEFTDOWN, NativeMethods.Enums.MOUSEEVENTF.LEFTUP)
                },
                {
                    WinApiMouseButton.Right,
                    Tuple.Create(NativeMethods.Enums.MOUSEEVENTF.RIGHTDOWN, NativeMethods.Enums.MOUSEEVENTF.RIGHTUP)
                }
            };
        }
    }
}