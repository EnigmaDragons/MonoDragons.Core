using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoDragons.Core.Engine
{
    public class MainGame : Game, INavigation
    {
        private readonly GraphicsDeviceManager _graphicsManager;
        private readonly SceneFactory _sceneFactory;

        private SpriteBatch _sprites;
        private IScene _currentScene;

        public MainGame(IScene startingView, ScreenSize screenSize, SceneFactory sceneFactory)
        {
            _graphicsManager = new GraphicsDeviceManager(this);
            screenSize.Apply(_graphicsManager);
            Content.RootDirectory = "Content";
            _currentScene = startingView;
            _sceneFactory = sceneFactory;
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
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _currentScene?.Update(gameTime.ElapsedGameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _sprites.Begin();
            _currentScene?.Draw();
            _sprites.End();
            base.Draw(gameTime);
        }

        public void NavigateTo(string sceneName)
        {
            _currentScene = _sceneFactory.Create(sceneName);
        }
    }
}
