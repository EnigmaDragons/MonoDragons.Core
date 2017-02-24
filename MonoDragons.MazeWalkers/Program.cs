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
            using (var game = new MainGame(new TextScene2(), new ScreenSize(900, 600), CreateSceneFactory()))
                game.Run();
        }

        private static SceneFactory CreateSceneFactory()
        {
            return new SceneFactory(
                new Dictionary<string, Func<IScene>>
                {
                    {"TextScene", () => new TextScene()},
                    {"TextScene2", () => new TextScene2()}
                });
        }
    }
#endif
}
