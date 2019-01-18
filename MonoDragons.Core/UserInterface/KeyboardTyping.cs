using System;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.Inputs.KeyboardEvents;

namespace MonoDragons.Core.UserInterface
{
    public sealed class KeyboardTyping : IAutomaton
    {
        private static readonly TimeSpan BackspaceRepeatInterval = TimeSpan.FromMilliseconds(60);

        private Action<string> _setValue = t => {};
        private string Result { get; set; }

        private TimeSpan _backspaceHeldDurationSinceInvocation;

        public KeyboardTyping(string startingValue = "")
        {
            Result = startingValue;
            Event.Subscribe<KeyboardCharacterInputted>(e => Result += e.Character.ToString(), this);
        }

        public KeyboardTyping OutputTo(Action<string> setValue)
        {
            _setValue = setValue;
            _setValue(Result);
            return this;
        }

        public void Update(TimeSpan delta)
        {
            UpdateBackspace(Keyboard.GetState(), delta);
            _setValue(Result);
        }

        private void UpdateBackspace(KeyboardState state, TimeSpan delta)
        {
            if (state.IsKeyDown(Keys.Back))
            {
                if (_backspaceHeldDurationSinceInvocation.Equals(TimeSpan.Zero))
                    Result = Result.Substring(0, Math.Max(0, Result.Length - 1));
                _backspaceHeldDurationSinceInvocation += delta;
                if (_backspaceHeldDurationSinceInvocation > BackspaceRepeatInterval)
                {
                    Result = Result.Substring(0, Math.Max(0, Result.Length - 1));
                    _backspaceHeldDurationSinceInvocation -= BackspaceRepeatInterval;
                }
            }
            if (!state.IsKeyDown(Keys.Back))
                _backspaceHeldDurationSinceInvocation = TimeSpan.Zero;
        }
    }
}
