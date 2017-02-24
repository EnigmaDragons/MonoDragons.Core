using System;
using System.Collections.Generic;
using MonoDragons.Core.Engine;
using MonoDragons.MazeWalkers.Scenes;

namespace MonoDragons.MazeWalkers
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new MainGame("ChronoScene", new ScreenSize(996, 1000), CreateSceneFactory()))
                game.Run();
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
