using System;
using System.Drawing;


namespace WinApiWrapper.Internal.Hooks
{
    internal class MoveHook
    {
        public Action<Point> HookMethod { get; set; }

        public MoveHook(Action<Point> hookMethod)
        {
            HookMethod = hookMethod;
        }
    }
}