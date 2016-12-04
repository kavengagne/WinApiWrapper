using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Windows.Forms;
using WinApiWrapper.Enums;
using WinApiWrapper.Models;
using WinApiWrapper.Native.Enums;
using WinApiWrapper.Native.Methods;


namespace WinApiWrapper.Wrappers
{
    public class WinApiKeyboard
    {
        private readonly Internal.Mappers.KeyboardMessagesTranslator _messageTranslator;
        private readonly ConcurrentDictionary<Guid, Internal.Hooks.KeyboardKeyHook> _keyHooks;
        private readonly HookHandle _keyboardHookHandle;


        public WinApiKeyboard()
        {
            _messageTranslator = new Internal.Mappers.KeyboardMessagesTranslator();
            _keyHooks = new ConcurrentDictionary<Guid, Internal.Hooks.KeyboardKeyHook>();
            _keyboardHookHandle = new HookHandle(HookType.WH_KEYBOARD_LL, HookWindowProc);
        }


        #region Hooks
        public Guid RegisterKeyHook(KeyboardKeyAction keyAction, Action<Keys> hookMethod)
        {
            return RegisterHook(_keyHooks, new Internal.Hooks.KeyboardKeyHook(keyAction, hookMethod));
        }

        public Guid RegisterKeyHook(KeyHook keyHook, Action<Keys> hookMethod)
        {
            return RegisterHook(_keyHooks, new Internal.Hooks.KeyboardKeyHook(keyHook.Action,
                key => InvokeKeySpecificHook(key, keyHook, hookMethod)));
        }

        public bool UnregisterKeyHook(Guid hookGuid)
        {
            return UnregisterHook(_keyHooks, hookGuid);
        }

        public void UnregisterAllKeyHooks()
        {
            _keyHooks.Clear();
        }

        public void UnregisterAllHooks()
        {
            UnregisterAllKeyHooks();
        }

        public static Keys ModifierKeys
        {
            get
            {
                Keys modifiers = 0;
                if (User32.GetKeyState((int)Keys.ShiftKey) < 0) modifiers |= Keys.Shift;
                if (User32.GetKeyState((int)Keys.ControlKey) < 0) modifiers |= Keys.Control;
                if (User32.GetKeyState((int)Keys.Menu) < 0) modifiers |= Keys.Alt;
                return modifiers;
            }
        }
        #endregion Hooks


        #region Hook Procedure
        private IntPtr HookWindowProc(int code, IntPtr wparam, IntPtr lparam)
        {
            if (code >= 0)
            {
                var key = _messageTranslator.GetKeyboardKey(lparam);
                if (IsButtonMessage(wparam) && !IsModifier(key))
                {
                    InvokeHooksInternal((WindowMessage)wparam, key);
                }
            }
            return User32.CallNextHookEx(_keyboardHookHandle, code, wparam, lparam);
        }
        #endregion Hook Procedure


        #region Private Methods
        private void InvokeHooksInternal(WindowMessage message, Keys key)
        {
            var action = _messageTranslator.GetKeyboardKeyAction(message);
            foreach (var hook in _keyHooks.Values.Where(hook => hook.KeyAction == action))
            {
                hook.HookMethod?.Invoke(key);
            }
        }

        private static void InvokeKeySpecificHook(Keys key, KeyHook keyHook, Action<Keys> hookMethod)
        {
            if (HasRequiredModifiers(keyHook.Modifiers) && key == keyHook.Key)
            {
                hookMethod.Invoke(key);
            }
        }

        private static bool IsButtonMessage(IntPtr wparam)
        {
            var message = (WindowMessage)wparam;
            switch (message)
            {
                case WindowMessage.KEYDOWN:
                case WindowMessage.KEYUP:
                    return true;
                default:
                    return false;
            }
        }

        private static bool IsModifier(Keys key)
        {
            return key == Keys.ControlKey || key == Keys.Menu || key == Keys.ShiftKey ||
                   key == Keys.LControlKey || key == Keys.RControlKey ||
                   key == Keys.LMenu || key == Keys.RMenu ||
                   key == Keys.LShiftKey || key == Keys.RShiftKey;
        }

        private static bool HasRequiredModifiers(Keys modifiers)
        {
            return modifiers == Keys.None || ModifierKeys.HasFlag(modifiers);
        }

        private static Guid RegisterHook<THook>(ConcurrentDictionary<Guid, THook> hookList, THook hook)
        {
            var guid = Guid.NewGuid();
            hookList.TryAdd(guid, hook);
            return guid;
        }

        private static bool UnregisterHook<THook>(ConcurrentDictionary<Guid, THook> hookList, Guid hookGuid)
        {
            THook removedHook;
            return hookList.TryRemove(hookGuid, out removedHook);
        }
        #endregion Private Methods
    }
}
