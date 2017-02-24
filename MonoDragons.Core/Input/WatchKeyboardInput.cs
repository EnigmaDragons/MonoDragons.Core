using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem.EventTypes;

namespace MonoDragons.Core.Input
{
    public class WatchKeyboardInput : IGameObject
    {
        private KeyboardState _lastState;

        public void Update(TimeSpan delta)
        {
            var state = Keyboard.GetState();
            var pressedKeys = state.GetPressedKeys().ToList();
            pressedKeys.Where(x => !_lastState.GetPressedKeys().Any(y => x.Equals(y))).ToList().ForEach(x => World.Publish(new KeyDownEvent(x)));
            _lastState.GetPressedKeys().Where(x => pressedKeys.All(y => !y.Equals(x)))
                .ToList().ForEach(x => World.Publish(new KeyUpEvent(x)));
            _lastState = state;
        }

        public void Draw(Vector2 offset)
        {
        }
    }
}
