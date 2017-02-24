using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        private string texture = "Images/Crono Nigga/Walk/down1(new).png";

        public Player()
        {
            new KeyDownEventSubscription(x => _isMovingUp = true, this, Keys.W).Subscribe();
            new KeyUpEventSubscription(x => _isMovingUp = false, this, Keys.W).Subscribe();
            new KeyDownEventSubscription(x => _isMovingDown = true, this, Keys.S).Subscribe();
            new KeyUpEventSubscription(x => _isMovingDown = false, this, Keys.S).Subscribe();
            new KeyDownEventSubscription(x => _isMovingRight = true, this, Keys.D).Subscribe();
            new KeyUpEventSubscription(x => _isMovingRight = false, this, Keys.D).Subscribe();
            new KeyDownEventSubscription(x => _isMovingLeft = true, this, Keys.A).Subscribe();
            new KeyUpEventSubscription(x => _isMovingLeft = false, this, Keys.A).Subscribe();
        }

        public void Update(TimeSpan delta)
        {
            var distance = (float)delta.TotalMilliseconds*_moveSpeed;
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
            if (_isMovingRight && !_isMovingLeft)
                texture = "Images/Crono Nigga/Walk/right1(new).png";
            else if (!_isMovingRight && _isMovingLeft)
                texture = "Images/Crono Nigga/Walk/left1(new).png";
            else if (_isMovingUp && !_isMovingDown)
                texture = "Images/Crono Nigga/Walk/up1(new).png";
            else if (!_isMovingUp && _isMovingDown)
                texture = "Images/Crono Nigga/Walk/down1(new).png";
            World.Draw(texture, _screenPositionOffset);
        }
    }
}
