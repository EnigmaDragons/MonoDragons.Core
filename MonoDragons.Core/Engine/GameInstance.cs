using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Common;

namespace MonoDragons.Core.Engine
{
    public static class GameInstance
    {
        private static readonly MustInit<Game> Game = new MustInit<Game>(nameof(GameInstance));

        public static Game TheGame => Game.Get();
        public static GraphicsDevice GraphicsDevice => TheGame.GraphicsDevice;
        public static ContentManager ContentManager => TheGame.Content;

        public static void Init(Game game) => Game.Init(game);
    }
}
