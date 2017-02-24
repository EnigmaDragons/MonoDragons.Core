using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem.Convience;

namespace MonoDragons.MazeWalkers.Scenes
{
    public class TextScene2 : IScene
    {
        public void Init()
        {
            new KeyDownEventSubscription(x => World.NavigateToScene("TextScene"), this, Keys.Enter).Subscribe();
            World.PlayMusic("Music/song1LOOP");
        }

        public void Update(TimeSpan delta)
        {
        }

        public void Draw()
        {
            World.DrawBrackgroundColor(Color.Aquamarine);
            World.DrawText("Hi Giovanni", new Vector2(200, 200), Color.Purple);
        }
    }
}
