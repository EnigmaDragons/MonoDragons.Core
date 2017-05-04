using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Inputs;
using MonoDragons.Core.Render;
using MonoDragons.MazeWalkers.Scenes;

namespace MonoDragons.MazeWalkers
{
#if WINDOWS || LINUX
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new MainGame("MazeWalkers", "ChronoScene", new Display(996, 1000, false), CreateSceneFactory(), CreateController()))
                game.Run();
        }

        private static IController CreateController()
        {
            return new KeyboardController(new Map<Keys, Control>());
        }

        private static SceneFactory CreateSceneFactory()
        {
            return new SceneFactory(
                new Dictionary<string, Func<IScene>>
                {
                    {"MainMenuScene", () => new MainMenuScene() },
                    {"ChronoScene", () => new ChronoScene() }
                });
        }
    }
#endif
}
