
namespace MonoDragons.Core.Physics
{
    public interface IPhysicsBody
    {
        bool IsRigid { get; }
        bool IsMobile { get; }
        Transform2 Transform { get; }
        Velocity2 Velocity { get; }
        
    }
}
