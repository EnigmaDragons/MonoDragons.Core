using System;
using MonoDragons.Core.Engine;

#if WINDOWS || LINUX

namespace MonoDragons.Core
{
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
