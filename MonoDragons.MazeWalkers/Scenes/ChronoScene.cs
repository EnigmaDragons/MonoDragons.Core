using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using System;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.MazeWalkers.Character;

namespace MonoDragons.MazeWalkers.Scenes
{
    public class ChronoScene : IScene
    {
        private Player _player = new Player();

        public void Init()
        {
        }

        public void Draw()
        {
            World.Draw("Images/Background/bg1", new Rectangle(0, 0, 1020, 996));
            _player.Draw(new Transform());
        }

        public void Update(TimeSpan delta)
        {
            _player.Update(delta);
        }
    }
}
