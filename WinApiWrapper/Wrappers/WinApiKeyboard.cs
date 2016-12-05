using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Windows.Forms;
using WinApiWrapper.Models;
using WinApiWrapper.Native.Enums;
using WinApiWrapper.Native.Methods;


namespace WinApiWrapper.Wrappers
{
    public class WinApiKeyboard : IDisposable
    {
        private readonly Internal.Mappers.KeyboardMessagesTranslator _messageTranslator;
        private readonly ConcurrentDictionary<Guid, Internal.Hooks.KeyboardHookBase> _keyHooks;
        private readonly HookHandle _keyboardHookHandle;


        public WinApiKeyboard()
        {
            _messageTranslator = new Internal.Mappers.KeyboardMessagesTranslator();
            _keyHooks = new ConcurrentDictionary<Guid, Internal.Hooks.KeyboardHookBase>();
            _keyboardHookHandle = new HookHandle(HookType.WH_KEYBOARD_LL, HookWindowProc);
        }


        #region Hooks
        /// <summary>
        /// Registers a hook which invokes the <paramref name="hookMethod"/> when the
        /// specified <paramref name="keyHook"/> is detected.
        /// </summary>
        public Guid RegisterKeyHook(Action hookMethod, KeyHook keyHook)
        {
            return RegisterKeyHook(hookMethod, new KeyChord(keyHook));
        }
        
        /// <summary>
        /// Registers a hook which invokes the <paramref name="hookMethod"/> when the
        /// specified <paramref name="keyChord"/> is satisfied.
        /// </summary>
        public Guid RegisterKeyHook(Action hookMethod, KeyChord keyChord)
        {
            return RegisterHook(_keyHooks, new Internal.Hooks.KeyboardKeyChordHook(keyChord, hookMethod));
        }

        /// <summary>
        /// Registers a hook which invokes the <paramref name="hookMethod"/> when the
        /// <c>KeyChord</c> created by the specified list of <c>KeyHook</c> is satisfied.
        /// </summary>
        public Guid RegisterKeyHook(Action hookMethod, IList<KeyHook> keyHooks)
        {
            return RegisterKeyHook(hookMethod, new KeyChord(keyHooks));
        }

        /// <summary>
        /// Registers a hook which invokes the <paramref name="hookMethod"/> when the
        /// <c>KeyChord</c> created by the specified array of <c>KeyHook</c> is satisfied.
        /// </summary>
        public Guid RegisterKeyHook(Action hookMethod, params KeyHook[] keyHooks)
        {
            return RegisterKeyHook(hookMethod, new KeyChord(keyHooks));
        }

        /// <summary>
        /// Unregisters an existing hook based on its <c>Guid</c>.
        /// </summary>
        public bool UnregisterKeyHook(Guid hookGuid)
        {
            return UnregisterHook(_keyHooks, hookGuid);
        }

        /// <summary>
        /// Unregisters all existing hooks.
        /// </summary>
        public void UnregisterAllHooks()
        {
            _keyHooks.Clear();
        }

        /// <summary>
        /// Returns the current number of hooks.
        /// </summary>
        public int HooksCount => _keyHooks.Count;

        /// <summary>
        /// Returns the current state of the modifier keys (Ctrl, Shift, Alt).
        /// </summary>
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

        /// <summary>
        /// Returns true if the <paramref name="key"/> is a modifier.
        /// </summary>
        /// <param name="key">The key to verify</param>
        /// <returns>Returns true if the <paramref name="key"/> is a modifier</returns>
        public static bool IsModifier(Keys key)
        {
            return key == Keys.ControlKey || key == Keys.Menu || key == Keys.ShiftKey ||
                   key == Keys.LControlKey || key == Keys.RControlKey ||
                   key == Keys.LMenu || key == Keys.RMenu ||
                   key == Keys.LShiftKey || key == Keys.RShiftKey;
        }
        #endregion Hooks


        #region Hook Procedure
        private IntPtr HookWindowProc(int code, IntPtr wparam, IntPtr lparam)
        {
            var message = (WindowMessage)wparam;
            if (code >= 0 && IsButtonMessage(message))
            {
                TryInvokeHooksInternal(message, lparam);
            }
            return User32.CallNextHookEx(_keyboardHookHandle, code, wparam, lparam);
        }
        #endregion Hook Procedure


        #region Private Methods
        private void TryInvokeHooksInternal(WindowMessage message, IntPtr lparam)
        {
            var key = _messageTranslator.GetKeyboardKey(lparam);
            var action = _messageTranslator.GetKeyboardKeyAction(message);
            foreach (var hook in _keyHooks.Values)
            {
                hook.TryInvoke(action, ModifierKeys, key);
            }
        }

        private static bool IsButtonMessage(WindowMessage message)
        {
            switch (message)
            {
                case WindowMessage.KEYDOWN:
                case WindowMessage.KEYUP:
                    return true;
                default:
                    return false;
            }
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


        public void Dispose()
        {
            UnregisterAllHooks();
            _keyboardHookHandle.Dispose();
        }
    }
}
