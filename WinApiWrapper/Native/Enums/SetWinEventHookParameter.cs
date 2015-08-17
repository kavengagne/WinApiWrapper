using System;

// ReSharper disable InconsistentNaming


namespace WinApiWrapper.Native.Enums
{
    [Flags]
    public enum SetWinEventHookParameter
    {
        WINEVENT_INCONTEXT = 4,
        WINEVENT_OUTOFCONTEXT = 0,
        WINEVENT_SKIPOWNPROCESS = 2,
        WINEVENT_SKIPOWNTHREAD = 1
    }
}