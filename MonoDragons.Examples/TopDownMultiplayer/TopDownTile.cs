using MonoDragons.Core;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Render;

namespace MonoDragons.Examples.TopDownMultiplayer
{
    public struct TopDownTile : IVisual
    {
        public Transform2 Transform { get; set; }
        public bool IsBlocking { get; set; }
        public int Layer { get; set; }
        public string Sprite { get; set; }
        
        public void Draw(Transform2 parentTransform)
        {
            World.Draw(Sprite, Transform + parentTransform);
        }
    }
}
