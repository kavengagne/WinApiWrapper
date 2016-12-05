using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WinApiWrapper.Enums;


namespace WinApiWrapper.Models
{
    public class KeyChord
    {
        private readonly IList<KeyHook> _hooks;
        private int _currentStep;


        public KeyChord(params KeyHook[] hooks) : this(hooks.ToList())
        {
        }

        public KeyChord(IList<KeyHook> hooks)
        {
            if (hooks == null || !hooks.Any())
            {
                throw new ArgumentException("A KeyChord must contain at least one KeyHook.", nameof(hooks));
            }
            _hooks = hooks;
        }

        internal bool TrySatisfy(KeyboardKeyAction action, Keys modifiers, Keys key)
        {
            var hook = _hooks[_currentStep];
            if (hook.Action == action)
            {
                if (hook.CanTrigger(action, modifiers, key))
                {
                    GoToNextStep();
                    if (IsChordSatisfied())
                    {
                        ResetChord();
                        return true;
                    }
                }
                else
                {
                    ResetChord();
                }
            }
            return false;
        }

        private bool IsChordSatisfied()
        {
            return _currentStep >= _hooks.Count;
        }

        private void GoToNextStep()
        {
            _currentStep++;
        }

        private void ResetChord()
        {
            _currentStep = 0;
        }
    }
}