using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem.Convience;
using MonoDragons.Core.UserInterface;

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
            UI.DrawBackgroundColor(Color.Black);
            UI.DrawText("Hi Tim", new Vector2(0, 0), Color.White);
        }
    }
}
