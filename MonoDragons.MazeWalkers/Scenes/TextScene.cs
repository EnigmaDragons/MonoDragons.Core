using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem.Convience;

namespace MonoDragons.MazeWalkers.Scenes
{
    public class TextScene : IScene
    {
        public void Init()
        {
            new KeyDownEventSubscription(x => World.NavigateToScene("TextScene2"), this, Keys.Enter).Subscribe();
            World.PlayMusic("Music/menuLOOP");
        }

        public void Update(TimeSpan delta)
        {
        }

        public void Draw()
        {
            World.DrawBrackgroundColor(Color.Black);
            World.DrawText("Hi Tim", new Vector2(0, 0), Color.White);
        }
    }
}
