using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Inputs;
using MonoDragons.MazeWalkers.Scenes;

namespace MonoDragons.MazeWalkers
{
#if WINDOWS || LINUX
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new MainGame("ChronoScene", new ScreenSettings(996, 1000, false), CreateSceneFactory(), CreateController()))
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
                    {"TextScene", () => new TextScene()},
                    {"TextScene2", () => new TextScene2()},
                    {"ChronoScene", () => new ChronoScene() }
                });
        }
    }
#endif
}
