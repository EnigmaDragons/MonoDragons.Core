using System;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;

namespace MonoDragons.MazeWalkers.Scenes
{
    public class TextScene : IScene
    {
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
