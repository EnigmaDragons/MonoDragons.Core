using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoDragons.MazeWalkers.Scenes
{
    class ChronoScene : IScene
    {
        public ChronoScene()
        {
        }

        public void Draw()
        {
            World.DrawBrackgroundColor(Color.Purple);
        }

        public void Update(TimeSpan delta)
        {
        }
    }
}
