using System;
using MonoDragons.Core.Engine;

namespace MonoDragons.Core
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
            using (var game = new MainGame(null, new ScreenSize(500, 400)))
                game.Run();
        }
    }
#endif
}
