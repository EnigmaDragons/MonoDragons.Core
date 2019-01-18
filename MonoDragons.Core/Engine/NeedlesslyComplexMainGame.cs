using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.Development;
using MonoDragons.Core.Errors;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.Inputs;
using MonoDragons.Core.Inputs.KeyboardEvents;
using MonoDragons.Core.Memory;
using MonoDragons.Core.Network;
using MonoDragons.Core.Physics;
using MonoDragons.Core.Render;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.UserInterface;

namespace MonoDragons.Core.Engine
{
    public class NeedlesslyComplexMainGame : Game
    {
        private readonly string _startingViewName;
        private readonly GraphicsDeviceManager _graphics;
        private readonly IScene _scene;
        private readonly IController _controller;
        private readonly Metrics _metrics;
        private readonly IErrorHandler _errorHandler;
        private readonly Display _display;

        private SpriteBatch _worldSpriteBatch;
        private SpriteBatch _uiSpriteBatch;

        public NeedlesslyComplexMainGame(string title, string startingViewName, Display display,
            IScene scene, IController controller, IErrorHandler errorHandler)
        {
            _display = display;
            _errorHandler = errorHandler;
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _startingViewName = startingViewName;
            _scene = scene;
            _controller = controller;
#if DEBUG
            _metrics = new Metrics();
#endif
            Window.Title = title;
            Window.TextInput += (sender, e) => Event.Publish(new KeyboardCharacterInputted(e.Character));
        }

        protected override void Initialize()
        {
            try
            {
                Perf.Time($"{nameof(NeedlesslyComplexMainGame)}.Initialize", () =>
                {
                    CurrentGame.Init(this);
                    Resources.Init();
                    // @todo #1 Bug: Update the GraphicsDeviceManager in the constructor, to avoid the window being mispositioned and visibly changing size
                    CurrentDisplay.Init(_graphics, _display);
                    Window.Position = new Point((GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - CurrentDisplay.GameWidth) / 2,
                        (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - CurrentDisplay.GameHeight) / 2 - 40); // Delete this once the above issue is fixed
                    IsMouseVisible = true;
                    _uiSpriteBatch = new SpriteBatch(GraphicsDevice);
                    _worldSpriteBatch = new SpriteBatch(GraphicsDevice);
                    Input.SetController(_controller);
                    World.Init(_worldSpriteBatch);
                    UI.Init(_uiSpriteBatch); 
                    _scene.Init();
                    base.Initialize();
                });
            }
            catch (Exception e)
            {
                _errorHandler.Handle(new Exception("Error while Initializing MonoDragons Core engine", e));
            }
        }

        protected override void LoadContent()
        {
            try
            {
                Scene.NavigateTo(_startingViewName);
            }
            catch (Exception e)
            {
                _errorHandler.Handle(new Exception($"Error while navigating to Scene {_scene.GetType()}", e));
            };
        }

        protected override void UnloadContent()
        {
            try
            {
                Content.Unload();
            }
            catch (Exception e)
            {
                _errorHandler.Handle(e);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            try
            {
#if DEBUG
                _metrics.Update(gameTime.ElapsedGameTime);
                if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.OemTilde))
                    Environment.Exit(0);
#endif
                Multiplayer.Update(gameTime.ElapsedGameTime);
                _controller.Update(gameTime.ElapsedGameTime);
                _scene.Update(gameTime.ElapsedGameTime);
            }
            catch (Exception e)
            {
                _errorHandler.Handle(new Exception("Error in Update Loop", e));
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            try
            {
                _uiSpriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.AnisotropicClamp);
                _worldSpriteBatch.Begin(samplerState: SamplerState.PointClamp);
                GraphicsDevice.Clear(Color.Black);
                _scene.Draw();
#if DEBUG
                _metrics.Draw(Transform2.Zero);
#endif
                _worldSpriteBatch.End();
                _uiSpriteBatch.End();
            }
            catch (Exception e)
            {
                _errorHandler.Handle(new Exception("Error in Draw Loop", e));
            }
        }
    }
}

