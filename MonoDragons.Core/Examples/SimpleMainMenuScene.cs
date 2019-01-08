using System;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Animations;
using MonoDragons.Core.AudioSystem;
using MonoDragons.Core.Inputs;
using MonoDragons.Core.Physics;
using MonoDragons.Core.Render;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.Text;
using MonoDragons.Core.UserInterface;

namespace MonoDragons.Core.Examples
{
    public sealed class MainMenuScene : ClickUiScene
    {
        private readonly string _newGameScene;

        public MainMenuScene(string newGameScene)
        {
            _newGameScene = newGameScene;
        }
        
        public override void Init()
        {
            Input.On(Control.Menu, () => Environment.Exit(0));
            Input.On(Control.Start, StartNewGame);
            Input.On(Control.Select, StartNewGame);
            Add(new UiImage
            {
                Image = "Images/SuperDragonAttack",
                Transform = new Transform2(new Size2(1.0.VW(), 1.0.VW()*9/16))
            });
            Add(new Label 
                { 
                    BackgroundColor = Color.FromNonPremultiplied(208, 42, 208, 160),
                    Font = DefaultFont.Header,
                    Text = "Super Dragon Attack", 
                    TextColor = Color.White, 
                    Transform = new Transform2(new Vector2(0.1.VW(), 0.1.VH()), new Size2(0.8.VW(), 0.05.VH()))
                });

            Add(MakeButton("New Game", new Point(0.05.VW(), 0.7.VH()), StartNewGame));
            Add(MakeButton("Exit Game", new Point(0.05.VW(), 0.8.VH()), StartNewGame));
            Add(new ScreenFade {Duration = TimeSpan.FromSeconds(1)}.Started());
        }

        private void StartNewGame()
        {
            Scene.NavigateTo(_newGameScene);
        }

        private TextButton MakeButton(string text, Point position, Action action)
        {
            return new TextButton(
                new Rectangle(position, new Point(300, 50)),
                action, 
                text,
                Color.FromNonPremultiplied(208, 42, 208, 160),
                Color.FromNonPremultiplied(208, 42, 208, 100),
                Color.FromNonPremultiplied(208, 42, 208, 40)
            );
        }
    }
}