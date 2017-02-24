using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.Engine;
using System;

namespace MonoDragons.MazeWalkers.Scenes
{
    public sealed class MainMenuScene : IScene
    {
        private INavigation _navigation;
        private bool _enterKeyPressed;

        public void Draw()
        {
            World.DrawBrackgroundColor(Color.Black);
            World.Draw("Images/Background/Maze", new Vector2(58, 0));
            World.DrawText("MazeWalker", new Vector2(610, 320), Color.White);
            World.DrawText("Press ENTER to begin", new Vector2(570, 380), Color.White);
        }

        public void Update(TimeSpan delta)
        {
            var ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Enter) && !_enterKeyPressed) { }
            _enterKeyPressed = ks.IsKeyDown(Keys.Enter);
        }

        public void SetNavigaton(INavigation navigation)
        {
            _navigation = navigation;
        }

        public void Init()
        {
            
        }
    }
}
