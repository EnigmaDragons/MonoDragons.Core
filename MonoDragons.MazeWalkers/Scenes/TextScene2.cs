using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem.Convience;
using MonoDragons.Core.Input;

namespace MonoDragons.MazeWalkers.Scenes
{
    public class TextScene2 : IScene
    {
        private readonly WatchKeyboardInput _watchKeyboardInput = new WatchKeyboardInput();

        public void Init()
        {
            new KeyDownEventSubscription(x => World.NavigateToScene("TextScene2"), this, Keys.Enter).Subscribe();
        }

        public void Update(TimeSpan delta)
        {
            _watchKeyboardInput.Update(delta);
        }

        public void Draw()
        {
            World.DrawBrackgroundColor(Color.Aquamarine);
            World.DrawText("Hi Giovanni", new Vector2(200, 200), Color.Purple);
        }
    }
}
