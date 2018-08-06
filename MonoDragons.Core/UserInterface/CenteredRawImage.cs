using MonoDragons.Core.Engine;
using MonoDragons.Core.Physics;

namespace MonoDragons.Core.UserInterface
{
    public sealed class CenteredRawImage : IVisual
    {
        private readonly string _image;

        public CenteredRawImage(string image)
        {
            _image = image;
        }
        
        public void Draw(Transform2 parentTransform)
        {
            UI.DrawCentered(_image);
        }
    }
}