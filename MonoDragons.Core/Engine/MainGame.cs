using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.Input;

namespace MonoDragons.Core.Engine
{
    public class MainGame : Game, INavigation
    {
        private readonly GraphicsDeviceManager _graphicsManager;
        private readonly string _startingViewName;
        private readonly SceneFactory _sceneFactory;
        private readonly WatchKeyboardInput watchForInput;

        private SpriteBatch _sprites;
        private IScene _currentScene;

        public MainGame(string startingViewName, ScreenSize screenSize, SceneFactory sceneFactory)
        {
            _graphicsManager = new GraphicsDeviceManager(this);
            screenSize.Apply(_graphicsManager);
            Content.RootDirectory = "Content";
            _startingViewName = startingViewName;
            _sceneFactory = sceneFactory;
            watchForInput = new WatchKeyboardInput();
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;
            _sprites = new SpriteBatch(GraphicsDevice);
            World.Init(this, this, _sprites);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            IsMouseVisible = true;
            NavigateTo(_startingViewName);
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            watchForInput.Update(gameTime.ElapsedGameTime);
            _currentScene?.Update(gameTime.ElapsedGameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _sprites.Begin();
            watchForInput.Draw(Vector2.Zero);
            _currentScene?.Draw();
            _sprites.End();
            base.Draw(gameTime);
        }

        public void NavigateTo(string sceneName)
        {
            var scene = _sceneFactory.Create(sceneName);
            scene.Init();
            _currentScene = scene;
        }
    }
}
