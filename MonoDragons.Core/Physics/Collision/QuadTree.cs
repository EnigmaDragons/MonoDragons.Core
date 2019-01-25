using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MonoDragons.Core.Physics
{
    public sealed class Quadtree {

        private int MAX_OBJECTS = 10;
        private int MAX_LEVELS = 5;

        private readonly int _depth;
        private readonly List<IPhysicsBody> _objects;
        private readonly Rectangle _bounds;
        private readonly Quadtree[] _nodes;

        public Quadtree(int depth, Rectangle nodeBounds) 
        {
            _depth = depth;
            _objects = new List<IPhysicsBody>();
            _bounds = nodeBounds;
            _nodes = new Quadtree[4];
        }
        
        public List<IPhysicsBody> Retrieve(List<IPhysicsBody> returnObjects, Rectangle pRect) 
        {
            var index = GetIndex(pRect);
            if (index != -1 && _nodes[0] != null) 
                _nodes[index].Retrieve(returnObjects, pRect);

            returnObjects.AddRange(_objects);

            return returnObjects;
        }
        
        public void Insert(IPhysicsBody p) 
        {
            if (_nodes[0] != null) {
                int index = GetIndex(p.Transform.ToRectangle());

                if (index != -1) {
                    _nodes[index].Insert(p);
                    return;
                }
            }

            _objects.Add(p);

            if (_objects.Count <= MAX_OBJECTS || _depth >= MAX_LEVELS) 
                return;
            
            if (_nodes[0] == null)  
                Split(); 

            var i = 0;
            while (i < _objects.Count)
            {
                var obj = _objects[i];
                var index = GetIndex(obj.Transform.ToRectangle());
                if (index != -1)
                {
                    _objects.Remove(obj);
                    _nodes[index].Insert(obj);
                }
                else 
                    i++;
            }
        }
        
        private int GetIndex(Rectangle pRect) 
        {
            int index = -1;
            double verticalMidpoint = _bounds.X + (_bounds.Width / 2);
            double horizontalMidpoint = _bounds.Y + (_bounds.Height / 2);

            // Object can completely fit within the top quadrants
            var topQuadrant = (pRect.Y < horizontalMidpoint && pRect.Y + pRect.Height < horizontalMidpoint);
            // Object can completely fit within the bottom quadrants
            var bottomQuadrant = (pRect.Y > horizontalMidpoint);

            // Object can completely fit within the left quadrants
            if (pRect.X < verticalMidpoint && pRect.X + pRect.Width < verticalMidpoint) {
                if (topQuadrant) {
                    index = 1;
                }
                else if (bottomQuadrant) {
                    index = 2;
                }
            }
            // Object can completely fit within the right quadrants
            else if (pRect.X > verticalMidpoint) {
                if (topQuadrant) {
                    index = 0;
                }
                else if (bottomQuadrant) {
                    index = 3;
                }
            }

            return index;
        }
        
        public void Clear() 
        {
            _objects.Clear();

            for (var i = 0; i < _nodes.Length; i++) {
                if (_nodes[i] != null) {
                    _nodes[i].Clear();
                    _nodes[i] = null;
                }
            }
        }
        
        private void Split() 
        {
            var subWidth = _bounds.Width / 2;
            var subHeight = _bounds.Height / 2;
            var x = _bounds.X;
            var y = _bounds.Y;

            _nodes[0] = new Quadtree(_depth+1, new Rectangle(x + subWidth, y, subWidth, subHeight));
            _nodes[1] = new Quadtree(_depth+1, new Rectangle(x, y, subWidth, subHeight));
            _nodes[2] = new Quadtree(_depth+1, new Rectangle(x, y + subHeight, subWidth, subHeight));
            _nodes[3] = new Quadtree(_depth+1, new Rectangle(x + subWidth, y + subHeight, subWidth, subHeight));
        }
    }
}
