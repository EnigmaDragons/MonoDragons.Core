using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Graphics;
using MonoDragons.Core.Memory;
using MonoDragons.Core.PhysicsEngine;
using System;

namespace MonoDragons.Core.UserInterface
{
    public sealed class ColoredRectangle : IVisual, IDisposable
    {
        private Color _color;
        private Texture2D _background;

        public Transform2 Transform { get; set; }

        public Color Color
        {
            get => _color;
            set
            {
                _color = value;
                UpdateTexture();
            }
        }

        public ColoredRectangle()
        {
            Transform = new Transform2(new Size2(400, 100));
            _background = new RectangleTexture(Color.Orange).Create();
        }

        public void Draw(Transform2 parentTransform)
        {
            var position = parentTransform + Transform;
            World.Draw(_background, position.ToRectangle());
        }

        private void UpdateTexture()
        {
            Resources.Dispose(_background);
            _background = new RectangleTexture(_color).Create();
        }

        public void Dispose()
        {
            Resources.Dispose(_background);
        }
    }
}