using System;
using Microsoft.Xna.Framework;

namespace MonoDragons.Core.Engine
{
    public interface IGameObject
    {
        void Update(TimeSpan delta);
        void Draw(Vector2 offset);
    }
}
