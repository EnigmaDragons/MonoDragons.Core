﻿using System;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Physics;

namespace MonoDragons.Core.UserInterface
{
    public sealed class Nothing : IVisualAutomaton
    {
        public void Update(TimeSpan delta)
        {
        }

        public void Draw(Transform2 parentTransform)
        {
        }
    }
}
