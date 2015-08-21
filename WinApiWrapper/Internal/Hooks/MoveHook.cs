using System;
using System.Drawing;


namespace WinApiWrapper
{
    internal partial class Internal
    {
        internal partial class Hooks
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
    }
}