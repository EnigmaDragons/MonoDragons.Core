using Microsoft.Xna.Framework;
using MonoDragons.Core.Physics;
using MonoDragons.Core.UserInterface;

namespace MonoDragons.Core.Scenes
{
    public sealed class LoadingScene : SimpleScene
    {
        public override void Init()
        {
            Add(new UiColoredRectangle { Color = Color.Black, Transform = new Transform2(new Size2(5000, 5000))});
        }
    }
}