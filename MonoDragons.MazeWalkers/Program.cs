using System;
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
            using (var game = new MainGame(new ChronoScene(), new ScreenSize(900, 600)))
                game.Run();
        }
    }
#endif
}
