﻿using System;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Physics;
using MonoDragons.Core.Render;

namespace MonoDragons.Core.UserInterface
{
    public class SingleImageButton : ClickableUIElement, IVisual
    {
        private readonly string _image;
        
        private readonly Color _hover;
        private readonly Color _press;

        private readonly Transform2 _transform;
        private readonly Action _onClick;

        private readonly ColoredRectangle _current;

        public SingleImageButton(string image, Color hover, Color press, Transform2 transform, Action onClick) : base(transform.ToRectangle())
        {
            _image = image;
            _hover = hover;
            _press = press;
            _transform = transform;
            _onClick = onClick;

            _current = new ColoredRectangle
            {
                Color = Color.Transparent,
                Transform = transform
            };
        }

        public void Draw(Transform2 parentTransform)
        {
            GameWorld.Draw(_image, parentTransform + _transform);
            _current.Draw(parentTransform);
        }

        public override void OnEntered()
        {
            _current.Color = _hover;
        }

        public override void OnExitted()
        {
            _current.Color = Color.Transparent;
        }

        public override void OnPressed()
        {
            _current.Color = _press;
        }

        public override void OnReleased()
        {
            _current.Color = Color.Transparent;
            _onClick.Invoke();
        }
    }
}
