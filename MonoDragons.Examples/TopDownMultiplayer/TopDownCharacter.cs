using Microsoft.Xna.Framework;
using MonoDragons.Core;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Inputs;
using MonoDragons.Core.Physics;
using MonoDragons.Core.Render;

namespace MonoDragons.Examples.TopDownMultiplayer
{
    public sealed class TopDownCharacter : IPhysicsBody, IVisual
    {
        private Direction _dir;
        private float _speed;

        public TopDownCharacter SpawnAt(Point worldPos)
        {
            Transform.Location = TopDownWorld.RawPositionOf(worldPos);
            return this;
        } 
        
        public TopDownCharacter UseController(IController controller)
        {
            Input.OnDirection(controller, OnDirection);
            return this;
        }

        public void OnDirection(Direction e)
        {
            _dir = e;
            _speed = 0.37f * (e.IsNeutral() ? 0 : 1);
        }

        public void Draw(Transform2 parentTransform)
        {
            World.Draw("TopDownMultiplayer/char-walk-d1", Transform + parentTransform);
        }

        public bool IsRigid => true;
        public bool IsMobile => true;
        public Transform2 Transform { get; } = new Transform2(TopDownWorld.TileSize);
        public Velocity2 Velocity => new Velocity2 { Angle = _dir.ToRotation(), Speed = _speed };
    }
}
