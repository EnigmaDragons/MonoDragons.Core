using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;

namespace MonoDragons.Core.Physics
{
    public sealed class PhysicsWorld : IAutomaton
    {
        private IReadOnlyCollection<IPhysicsBody> _current = new List<IPhysicsBody>();
        private readonly Quadtree _quadtree;
        private readonly Vector2 _worldMin;
        private readonly Vector2 _worldMax;

        public PhysicsWorld(int width, int height)
            : this(new Rectangle(0, 0, width, height)) { }
        
        public PhysicsWorld(Rectangle worldBounds)
        {
            _quadtree = new Quadtree(16, worldBounds);
            _worldMin = worldBounds.Location.ToVector2();
            _worldMax = (worldBounds.Location + worldBounds.Size).ToVector2();
        }
        
        public PhysicsWorld Add(IPhysicsBody b)
        {
            _current = _current.Append(b);
            return this;
        }

        public PhysicsWorld Remove(IPhysicsBody b)
        {
            _current = _current.Where(x => !ReferenceEquals(x, b)).ToList();
            return this;
        }
        
        public void Update(TimeSpan delta)
        {
            _current = GetNextState(delta);
        }

        private List<IPhysicsBody> GetNextState(TimeSpan delta)
        {
            return _current.Select(x =>
            {
                if (!x.IsMobile)
                    return x;
                else
                {
                    var movement = (x.Velocity.ToVector2() * delta.Milliseconds);
                    x.Transform.Location = Vector2.Clamp(x.Transform.Location + movement, _worldMin, _worldMax - x.Transform.Size.ToVector());
                    return x;
                }
            }).ToList();
        }
    }
}
