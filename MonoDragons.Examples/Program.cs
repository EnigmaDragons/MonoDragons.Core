﻿using System;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.AudioSystem;
using MonoDragons.Core.EngimaDragons;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Errors;
using MonoDragons.Core.Examples;
using MonoDragons.Core.Inputs;
using MonoDragons.Core.Memory;
using MonoDragons.Core.Network;
using MonoDragons.Core.Render;
using MonoDragons.Core.Scenes;
using MonoDragons.Examples.ScissorsPaperRock;
using MonoDragons.Examples.TopDownMultiplayer;

namespace MonoDragons.Examples
{
    internal class Program
    {
        public static readonly IErrorHandler ErrorHandler = new MessageBoxErrorHandler();

        public static readonly AppDetails AppDetails = new AppDetails("MonoDragons Examples", "0.0", Environment.OSVersion.VersionString);
        
        [STAThread]
        static void Main()
        {
            Error.Handle(() =>
            {
                using (var game = new NeedlesslyComplexMainGame(AppDetails.Name, "TopDown", new Display(1600, 900, false), SetupScene(), CreateKeyboardController(), ErrorHandler))
                    game.Run();
            }, ErrorHandler.Handle);
        }

        private static IScene SetupScene()
        {
            var currentScene = new CurrentScene();
            Scene.Init(new CurrentSceneNavigation(currentScene, CreateSceneFactory(),  
                AudioPlayer.Instance.StopAll, 
                Resources.Unload));
            return new HideViewportExternals(currentScene);
        }

        private static SceneFactory CreateSceneFactory()
        {
            return new SceneFactory(new Map<string, Func<IScene>>
            {
                { "Lobby", () => new LobbyScene()},
                { "TopDown", () => new TopDownMultiplayerGame() }
            });
        }

        private static IController CreateKeyboardController()
        {
            return new KeyboardController(new Map<Keys, Control>
            {
                { Keys.Space, Control.Select },
                { Keys.Enter, Control.Start },
                { Keys.V, Control.A },
                { Keys.O, Control.X }
            });
        }
    }
}
