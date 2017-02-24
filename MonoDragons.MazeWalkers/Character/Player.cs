using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem.Convience;

namespace MonoDragons.MazeWalkers.Character
{
    public class Player : IGameObject
    {
        private readonly float _moveSpeed = 0.24f;
        private Vector2 _screenPositionOffset = new Vector2(200, 200);

        private bool _isMovingUp;
        private bool _isMovingDown;
        private bool _isMovingRight;
        private bool _isMovingLeft; 

        public Player()
        {
            new KeyDownEventSubscription(x => { _isMovingUp = true; Console.WriteLine("Is Moving Up: " + _isMovingUp); }, this, Keys.W).Subscribe();
            new KeyUpEventSubscription(x => { _isMovingUp = false; Console.WriteLine("Is Moving Up: " + _isMovingUp); }, this, Keys.W).Subscribe();
            new KeyDownEventSubscription(x => { _isMovingDown = true; Console.WriteLine("Is Moving Down: " + _isMovingDown); }, this, Keys.S).Subscribe();
            new KeyUpEventSubscription(x => _isMovingDown = false, this, Keys.S).Subscribe();
            new KeyDownEventSubscription(x => _isMovingRight = true, this, Keys.D).Subscribe();
            new KeyUpEventSubscription(x => _isMovingRight = false, this, Keys.D).Subscribe();
            new KeyDownEventSubscription(x => _isMovingLeft = true, this, Keys.A).Subscribe();
            new KeyUpEventSubscription(x => _isMovingLeft = false, this, Keys.A).Subscribe();
        }

        public void Update(TimeSpan delta)
        {
            var distance = delta.Ticks*_moveSpeed;
            Console.WriteLine("Is Moving Right: " + _isMovingUp);
            Console.WriteLine("Is Moving Left: " + _isMovingUp);
            if (_isMovingUp)
                _screenPositionOffset += new Vector2(0, -distance);
            if (_isMovingDown)
                _screenPositionOffset += new Vector2(0, distance);
            if (_isMovingRight)
                _screenPositionOffset += new Vector2(distance, 0);
            if (_isMovingLeft)
                _screenPositionOffset += new Vector2(-distance, 0);
        }

        public void Draw(Vector2 offset)
        {
            World.Draw("Images/Crono Nigga/Walk/down1(new).png", _screenPositionOffset);
            //sprites.Draw(anims.CurrentFrame, new Rectangle((int)screenPositionOffset.X, (int)screenPositionOffset.Y + 64, 64, 128), Color.White);
        }
    }
}
