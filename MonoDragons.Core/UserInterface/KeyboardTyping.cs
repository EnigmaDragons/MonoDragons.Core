using System;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.Inputs.KeyboardEvents;

namespace MonoDragons.Core.UserInterface
{
    public sealed class KeyboardTyping : IAutomaton
    {
        private static readonly TimeSpan backspaceRepeatInterval = TimeSpan.FromMilliseconds(60);

        private Action<string> _setValue = t => {};
        public string Result { get; private set; }

        private TimeSpan _backspaceHeldDurationSinceInvocation;

        public KeyboardTyping(string startingValue = "")
        {
            Result = startingValue;
            Event.Subscribe<KeyboardCharacterInputted>(e => Result += e.Character.ToString(), this);
        }

        public KeyboardTyping OutputTo(Action<string> setValue)
        {
            _setValue = setValue;
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
                _backspaceHeldDurationSinceInvocation += delta;
                if (_backspaceHeldDurationSinceInvocation > backspaceRepeatInterval)
                {
                    Result = Result.Substring(0, Math.Max(0, Result.Length - 1));
                    _backspaceHeldDurationSinceInvocation -= backspaceRepeatInterval;
                }
            }
            if (!state.IsKeyDown(Keys.Back))
                _backspaceHeldDurationSinceInvocation = TimeSpan.Zero;
        }
    }
}
