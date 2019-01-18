using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace MonoDragons.Core.Scenes
{
    public sealed class CurrentSceneNavigation : INavigation
    {
        private static readonly IScene LoadingScene = new LoadingScene();
        private readonly CurrentScene _currentScene;
        private readonly SceneFactory _sceneFactory;
        private readonly IEnumerable<Action> _beforeNavigate;
        private bool _isInitialized;

        public CurrentSceneNavigation(CurrentScene currentScene, SceneFactory sceneFactory, params Action[] beforeNavigate)
        {
            _currentScene = currentScene;
            _sceneFactory = sceneFactory;
            _beforeNavigate = beforeNavigate;
        }

        public void NavigateTo(string sceneName)
        {
            NavigateTo(_sceneFactory.Create(sceneName));
        }

        public void NavigateTo(IScene scene)
        {
            InitLoadingSceneIfNeeded();

            var previousScene = _currentScene;
            _currentScene.Set(LoadingScene);
            _beforeNavigate.ForEach(x => x());
            previousScene.Dispose();
            scene.Init();
            _currentScene.Set(scene);
        }

        private void InitLoadingSceneIfNeeded()
        {
            if (_isInitialized) return;
            
            LoadingScene.Init();
            _isInitialized = true;
        }
    }
}
