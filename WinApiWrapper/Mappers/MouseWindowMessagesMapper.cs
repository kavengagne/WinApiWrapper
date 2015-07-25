using System;
using System.Collections.Generic;
using WinApiWrapper.Enums;
using WinApiWrapper.Unsafe;

namespace WinApiWrapper.Mappers
{
    public class MouseWindowMessagesMapper :
        MapperBase<WinApiMouseButton, Tuple<NativeMethods.Enums.WindowMessages, NativeMethods.Enums.WindowMessages>>
    {
        public MouseWindowMessagesMapper()
        {
            Mappings = new Dictionary
                <WinApiMouseButton, Tuple<NativeMethods.Enums.WindowMessages, NativeMethods.Enums.WindowMessages>>
            {
                {
                    WinApiMouseButton.Left,
                    Tuple.Create(NativeMethods.Enums.WindowMessages.LBUTTONDOWN,
                                 NativeMethods.Enums.WindowMessages.LBUTTONUP)
                },
                {
                    WinApiMouseButton.Right,
                    Tuple.Create(NativeMethods.Enums.WindowMessages.RBUTTONDOWN,
                                 NativeMethods.Enums.WindowMessages.RBUTTONUP)
                }
            };
        }
    }
}