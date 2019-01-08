using System;
using MonoDragons.Core.Animations;
using MonoDragons.Core.Inputs;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.UserInterface;

namespace MonoDragons.Core.Examples
{
    public sealed class SimpleLogoScene : ClickUiScene
    {
        private readonly string _nextScene;
        private readonly string _logoImage;

        public SimpleLogoScene(string nextScene, string logoImage)
        {
            _nextScene = nextScene;
            _logoImage = logoImage;
        }

        public override void Init()
        {
            Input.On(Control.Start, NavigateToMainMenu);
            Input.On(Control.Select, NavigateToMainMenu);
            Add(new ScreenClickable(NavigateToMainMenu));

            var anim1 = new ScreenFade { Duration = TimeSpan.FromSeconds(3.4), FromAlpha = 255, ToAlpha = 0 };
            var anim2 = new ScreenFade { Duration = TimeSpan.FromSeconds(1), FromAlpha = 0, ToAlpha = 0 };
            var anim3 = new ScreenFade { Duration = TimeSpan.FromSeconds(1), FromAlpha = 0, ToAlpha = 255 };
            Add(new CenteredRawImage(_logoImage));
            Add(anim1);
            Add(anim2);
            Add(anim3);
            anim1.Start(() => anim2.Start(() => anim3.Start(NavigateToMainMenu)));
        }

        private void NavigateToMainMenu()
        {
            Scene.NavigateTo(_nextScene);
        }
    }
}
