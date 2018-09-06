using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoDragons.Core.Render
{
    public class Display
    {
        public bool UseFullscreen { get; private set; }
        public int GameWidth { get; private set; }
        public int GameHeight { get; private set; }
        private int ProgramWidth { get; set; }
        private int ProgramHeight { get; set; }
        public float Scale { get; private set; }
        private bool _initialized;

        public Display(int width, int height, bool useFullscreen, float scale = 1)
        {
            UseFullscreen = useFullscreen;
            GameWidth = width;
            GameHeight = height;
            Scale = scale;
            ProgramWidth = GameWidth;
            ProgramHeight = GameHeight;
        }

        public void Apply(GraphicsDeviceManager deviceManager)
        {
            
            if (!_initialized && UseFullscreen)
            {
                
                var widthScale = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / GameWidth;
                var heightScale = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / GameHeight;
                var newScaleModifier = Convert.ToInt32(Math.Min(heightScale, widthScale));
                Scale = Scale * newScaleModifier;
                GameWidth = GameWidth * newScaleModifier;
                GameHeight = GameHeight * newScaleModifier;
                ProgramWidth = deviceManager.GraphicsDevice.DisplayMode.Width;
                ProgramHeight = deviceManager.GraphicsDevice.DisplayMode.Height;
                _initialized = true;
            }

            deviceManager.PreferredBackBufferWidth = ProgramWidth;
            deviceManager.PreferredBackBufferHeight = ProgramHeight;
            deviceManager.IsFullScreen = UseFullscreen;
            deviceManager.ApplyChanges();
        }
    }
}
