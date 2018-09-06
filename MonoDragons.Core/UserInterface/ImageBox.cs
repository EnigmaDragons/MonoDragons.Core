﻿using System;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Physics;
using MonoDragons.Core.Render;

namespace MonoDragons.Core.UserInterface
{
    public sealed class ImageBox : IVisual
    {
        public Transform2 Transform { get; set; }
        public string Image { get; set; } = "none";
        public Func<bool> IsActive { get; set; } = () => true;
        
        public void Clear()
        {
            Image = "none";
        }

        public void Draw(Transform2 parentTransform)
        {
            if (!"none".Equals(Image) && IsActive())
                GameWorld.Draw(Image, parentTransform + Transform);
        }
    }
}
