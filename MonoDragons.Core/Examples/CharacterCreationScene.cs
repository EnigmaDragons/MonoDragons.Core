using System;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.UserInterface;

namespace MonoDragons.Core.Examples
{
    public sealed class CharacterCreationScene : ClickUiScene
    {
        public override void Init()
        {
            Add(MakeButton("Done", UI.OfScreen(0.10f, 0.70f).ToPoint(), () => Scene.NavigateTo("MainMenu")));
            var nameLabel = new Label();
            Add(nameLabel);
            Add(new KeyboardTyping("Name").OutputTo(t => nameLabel.Text = t));
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
