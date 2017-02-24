using System;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;

namespace MonoDragons.MazeWalkers.Scenes
{
    public class TextScene2 : IScene
    {
        public void Init()
        {
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
