using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using WinApiWrapper.Enums;


namespace WinApiWrapper.Models
{
    public class KeyChord
    {
        private readonly IList<KeyHook> _hooks;
        private int _currentStep;
        private Timer _timer;

        private readonly int _delayMilliseconds;


        public KeyChord(IEnumerable<KeyHook> hooks, int delayMilliseconds = 500) : this(hooks.ToList(), delayMilliseconds)
        {
        }

        public KeyChord(IList<KeyHook> hooks, int delayMilliseconds = 500)
        {
            if (hooks == null || !hooks.Any())
            {
                throw new ArgumentException("A KeyChord must contain at least one KeyHook.", nameof(hooks));
            }
            _hooks = hooks;
            _delayMilliseconds = delayMilliseconds;
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
                        Debug.WriteLine("KeyChord satisfied");
                        ResetChord();
                        return true;
                    }
                    StartTimer();
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
            Debug.WriteLine("KeyChord step incremented: {0}/{1}", _currentStep, _hooks.Count);
        }

        private void StartTimer()
        {
            StopTimer();
            _timer = new Timer { Enabled = false, Interval = _delayMilliseconds };
            _timer.Tick += (sender, args) =>
            {
                StopTimer();
                Debug.WriteLine("KeyChord timer expired");
                ResetChord();
            };
            Debug.WriteLine("KeyChord timer started");
            _timer.Start();
        }

        private void StopTimer()
        {
            Debug.WriteLine("KeyChord timer stopped");
            _timer?.Stop();
        }

        private void ResetChord()
        {
            StopTimer();
            Debug.WriteLine("KeyChord reset");
            _currentStep = 0;
        }
    }
}