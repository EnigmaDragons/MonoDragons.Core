using System;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.AudioSystem;
using MonoDragons.Core.Common;
using MonoDragons.Core.Development;
using MonoDragons.Core.EngimaDragons;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Inputs;
using MonoDragons.Core.Memory;
using MonoDragons.Core.Render;
using MonoDragons.Core.Scenes;

namespace MonoDragons.Core
{
    public static class Demo
    {
        [STAThread]
        static void Main()
        {
            using (var game = Perf.Time("Startup", () => new NeedlesslyComplexMainGame("MonoDragons.Core", "Logo", new Display(1600, 900, false), SetupScene(), CreateKeyboardController())))
                game.Run();
        }

        private static CurrentScene SetupScene()
        {
            var currentScene = new CurrentScene();
            Scene.Init(new CurrentSceneNavigation(currentScene, CreateSceneFactory(), 
                Input.ClearTransientBindings, 
                Audio.StopMusic, 
                Resources.Unload));
            return currentScene;
        }

        private static SceneFactory CreateSceneFactory()
        {
            return new SceneFactory(new Map<string, Func<IScene>>
            {
                { "Logo", () => new FadingInScene(new OilLogoScene()) },
                { "Intro", () => new PlaceholderScene() },
            });
        }

        private static IController CreateKeyboardController()
        {
            return new KeyboardController(new Map<Keys, Control>
            {
                { Keys.OemTilde, Control.Select },
                { Keys.Enter, Control.Start },
                { Keys.V, Control.A },
                { Keys.O, Control.X }
            });
        }
    }
}
