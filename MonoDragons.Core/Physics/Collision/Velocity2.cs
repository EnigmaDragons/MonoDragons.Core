using Microsoft.Xna.Framework;

namespace MonoDragons.Core.Physics
{
    public sealed class Velocity2
    {
        public Rotation2 Angle { get; set; }
        public float Speed { get; set; }

        public Vector2 ToVector2()
        {
            // TODO: Use Trig to get correct speed for Horizontal + Vertical
            return Angle.ToDirection().AsOffset().ToVector2() * Speed;
        }
    }
}
