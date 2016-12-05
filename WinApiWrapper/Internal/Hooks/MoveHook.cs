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
                internal Action<Point> HookMethod { get; set; }

                internal MoveHook(Action<Point> hookMethod)
                {
                    HookMethod = hookMethod;
                }
            }
        }
    }
}