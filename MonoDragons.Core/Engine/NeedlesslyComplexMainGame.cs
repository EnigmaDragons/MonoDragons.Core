using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Inputs;
using MonoDragons.Core.Memory;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Render;
using MonoDragons.Core.UserInterface;
using System;
using MonoDragons.Core.Development;
using MonoDragons.Core.Scenes;
using System.Windows.Forms;

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

        // @todo #1 fix this so we config everything before the game
        public NeedlesslyComplexMainGame(string title, string startingViewName, Size2 defaultGameSize, IScene scene, IController controller)
            : this(title, startingViewName, scene, controller)
        {
            _areScreenSettingsPreCalculated = false;
            _defaultScreenSize = defaultGameSize;
        }

        public NeedlesslyComplexMainGame(string title, string startingViewName, Display screenSettings, IScene scene, IController controller)
            : this(title, startingViewName, scene, controller)
        {
            _areScreenSettingsPreCalculated = true;
            _display = screenSettings;
        }

        private NeedlesslyComplexMainGame(string title, string startingViewName, IScene scene, IController controller)
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
                CurrentGame.Init(this);
                Resources.Init();
                InitDisplayIfNeeded();
                // @todo #1 Bug: Update the GraphicsDeviceManager in the constructor, to avoid the window being mispositioned and visibly changing size
                CurrentDisplay.Init(_graphics, _display);
                Window.Position = new Point(0, 0); // Delete this once the above issue is fixed
                IsMouseVisible = true;
                _sprites = new SpriteBatch(GraphicsDevice);
                Input.SetController(_controller);
                World.Init(_sprites);
                UI.Init(_sprites);
                _scene.Init();
                base.Initialize();
            });
        }

        private void InitDisplayIfNeeded()
        {
            if (!_areScreenSettingsPreCalculated)
                _display = new Display(_defaultScreenSize.Width, _defaultScreenSize.Height, true, 1);
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
        }

        protected override void Draw(GameTime gameTime)
        {
            _sprites.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            GraphicsDevice.Clear(Color.Black);
            _scene.Draw();
            //World
#if DEBUG
            _metrics.Draw(Transform2.Zero);
#endif
            _sprites.End();
        }
    }
}
