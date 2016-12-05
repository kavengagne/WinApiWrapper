using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinApiWrapper.Enums;


namespace WinApiWrapper
{
    internal partial class Internal
    {
        internal partial class Hooks
        {
            internal abstract class KeyboardHookBase
            {
                internal Action HookMethod { get; set; }

                protected KeyboardHookBase(Action hookMethod)
                {
                    HookMethod = hookMethod;
                }

                internal bool TryInvoke(KeyboardKeyAction action, Keys modifiers, Keys key)
                {
                    if (CanInvoke(action, modifiers, key))
                    {
                        Debug.WriteLine("Invoking method for Action: {0}, Modifiers: {1}, Key: {2}", action, modifiers, key);
                        Task.Run((Action)Invoke);
                        return true;
                    }
                    return false;
                }

                protected abstract bool CanInvoke(KeyboardKeyAction action, Keys modifiers, Keys key);

                protected virtual void Invoke()
                {
                    HookMethod?.Invoke();
                }
            }
        }
    }
}
