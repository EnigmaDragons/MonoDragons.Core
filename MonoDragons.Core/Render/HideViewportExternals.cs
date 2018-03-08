using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Graphics;
using MonoDragons.Core.Scenes;
using System;
using MonoDragons.Core.Common;

namespace MonoDragons.Core.Render
{
    public sealed class HideViewportExternals : IScene
    {
        private readonly IScene _inner;
        private readonly MustInit<Texture2D> _black = new MustInit<Texture2D>(nameof(HideViewportExternals));

        public HideViewportExternals(IScene inner)
        {
            _inner = inner;
        }

        public void Init()
        {
            _inner.Init();
            _black.Init(new RectangleTexture(Color.Black).Create());
        }

        public void Update(TimeSpan delta)
        {
            _inner.Update(delta);
        }

        public void Draw()
        {
            _inner.Draw();
            HideExternals();
        }

        private void HideExternals()
        {
            var display = CurrentDisplay.Get();
            World.Draw(_black.Get(), new Rectangle(new Point(display.GameWidth, 0),
                new Point(display.ProgramWidth - display.GameWidth, display.ProgramHeight)), Color.Black);
            World.Draw(_black.Get(), new Rectangle(new Point(0, display.GameHeight),
                new Point(display.ProgramWidth, display.ProgramHeight - display.GameHeight)), Color.Black);
        }
    }
}