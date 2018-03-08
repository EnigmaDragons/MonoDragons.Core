using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Graphics;
using MonoDragons.Core.Inputs;
using MonoDragons.Core.Memory;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Render;
using MonoDragons.Core.UserInterface;
using System;
using MonoDragons.Core.Development;
using MonoDragons.Core.Scenes;

namespace MonoDragons.Core.Engine
{
    public class NeedlesslyComplexMainGame : Game
    {
        private readonly string _startingViewName;
        private readonly GraphicsDeviceManager _graphics;
        private readonly IScene _scene;
        private readonly IController _controller;
        private readonly Metrics _metrics;
        private readonly bool _areScreenSettingsPreCalculated;
        
        private SpriteBatch _sprites;
        private Display _display;
        private Size2 _defaultScreenSize;
        private Texture2D _black;

        // @todo #1 fix this so we config everything before the game
        public NeedlesslyComplexMainGame(string title, string startingViewName, Size2 defaultGameSize, CurrentScene scene, IController controller)
            : this(title, startingViewName, scene, controller)
        {
            _areScreenSettingsPreCalculated = false;
            _defaultScreenSize = defaultGameSize;
        }

        public NeedlesslyComplexMainGame(string title, string startingViewName, Display screenSettings, CurrentScene scene, IController controller)
            : this(title, startingViewName, scene, controller)
        {
            _areScreenSettingsPreCalculated = true;
            _display = screenSettings;
        }

        private NeedlesslyComplexMainGame(string title, string startingViewName, CurrentScene scene, IController controller)
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _startingViewName = startingViewName;
            _scene = scene;
            _controller = controller;
#if DEBUG
            _metrics = new Metrics();
#endif

            Window.Title = title;
        }

        protected override void Initialize()
        {
            Perf.Time($"{nameof(NeedlesslyComplexMainGame)}.Initialize", () =>
            {
                GameInstance.Init(this);
                Resources.Init();
                InitDisplayIfNeeded();
                // @todo #1 Bug: Update the GraphicsDeviceManager in the constructor, to avoid the window being mispositioned and visibly changing size
                _display.Apply(_graphics);
                Window.Position = new Point(0, 0); // Delete this once the above issue is fixed
                IsMouseVisible = true;
                _sprites = new SpriteBatch(GraphicsDevice);
                Input.SetController(_controller);
                _black = new RectangleTexture(Color.Black).Create();
                World.Init(_sprites, _display);
                UI.Init(this, _sprites, _display);
                base.Initialize();
            });
        }

        private void InitDisplayIfNeeded()
        {
            if (!_areScreenSettingsPreCalculated)
            {
                var widthScale = (float)GraphicsDevice.DisplayMode.Width / _defaultScreenSize.Width;
                var heightScale = (float)GraphicsDevice.DisplayMode.Height / _defaultScreenSize.Height;
                var scale = widthScale > heightScale ? heightScale : widthScale;
                _display = new Display((int)Math.Round(_defaultScreenSize.Width * scale), (int)Math.Round(_defaultScreenSize.Height * scale),
                    true, scale);
            }
            CurrentDisplay.Init(_display);
        }

        protected override void LoadContent()
        {
            Scene.NavigateTo(_startingViewName);
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
#if DEBUG
            _metrics.Update(gameTime.ElapsedGameTime);
#endif
            _controller.Update(gameTime.ElapsedGameTime);
            _scene.Update(gameTime.ElapsedGameTime);
            new Physics().Resolve();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _sprites.Begin(SpriteSortMode.Deferred, null, SamplerState.AnisotropicClamp);
            GraphicsDevice.Clear(Color.Black);
            _scene.Draw();
#if DEBUG
            _metrics.Draw(Transform2.Zero);
#endif
            HideExternals();
            _sprites.End();
            base.Draw(gameTime);
        }

        private void HideExternals()
        {
            _sprites.Draw(_black, new Rectangle(new Point(_display.GameWidth, 0),
                new Point(_display.ProgramWidth - _display.GameWidth, _display.ProgramHeight)), Color.Black);
            _sprites.Draw(_black, new Rectangle(new Point(0, _display.GameHeight),
                new Point(_display.ProgramWidth, _display.ProgramHeight - _display.GameHeight)), Color.Black);
        }
    }
}
