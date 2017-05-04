using System;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Inputs;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.MazeWalkers.Character
{
    public class Player : IVisualAutomaton
    {
        private readonly float _moveSpeed = 0.24f;
        private Vector2 _screenPositionOffset = new Vector2(200, 200);

        private Direction _currentDirection = Direction.None;

        private string texture = "Images/Crono Nigga/Walk/down1(new).png";

        public Player()
        {
            Input.OnDirection(x => _currentDirection = x);
        }

        public void Update(TimeSpan delta)
        {
            var distance = (float)delta.TotalMilliseconds*_moveSpeed;
                _screenPositionOffset += new Vector2((int)_currentDirection.HDir * distance, (int)_currentDirection.VDir * distance);
        }

        public void Draw(Transform2 transform)
        {
            if (_currentDirection.HDir.Equals(HorizontalDirection.Left))
                texture = "Images/Crono Nigga/Walk/left1(new).png";
            if (_currentDirection.HDir.Equals(HorizontalDirection.Right))
                texture = "Images/Crono Nigga/Walk/right1(new).png";
            if (_currentDirection.VDir.Equals(VerticalDirection.Up))
                texture = "Images/Crono Nigga/Walk/up1(new).png";
            if (_currentDirection.VDir.Equals(VerticalDirection.Down))
                texture = "Images/Crono Nigga/Walk/down1(new).png";
            World.Draw(texture, _screenPositionOffset);
        }
    }
}
