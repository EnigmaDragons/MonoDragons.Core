using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using System;
using MonoDragons.Core.Input;
using MonoDragons.MazeWalkers.Character;

namespace MonoDragons.MazeWalkers.Scenes
{
    public class ChronoScene : IScene
    {
        private Player _player = new Player();
        private WatchKeyboardInput _watchKeyboardInput = new WatchKeyboardInput();

        public void Init()
        {
        }

        public void Draw()
        {
            World.DrawBrackgroundColor(Color.Purple);
            World.Draw("Images/Background/bg1", new Rectangle(0, 0, 1020, 996));
            _watchKeyboardInput.Draw(Vector2.Zero);
            _player.Draw(new Vector2(0, 0));
        }

        public void Update(TimeSpan delta)
        {
            _watchKeyboardInput.Update(delta);
            _player.Update(delta);
        }
    }
}
