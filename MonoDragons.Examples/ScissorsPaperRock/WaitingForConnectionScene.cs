using Microsoft.Xna.Framework;
using MonoDragons.Core;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.Network;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.UserInterface;

namespace MonoDragons.Examples.ScissorsPaperRock
{
    public sealed class WaitingForConnectionScene : ClickUiScene
    {
        private readonly string _message;

        public WaitingForConnectionScene(string message)
        {
            _message = message;
        }

        public override void Init()
        {
            Add(new Label { Text = "Waiting For Connection", Transform = new Transform2(new Vector2(0, 0), new Size2(400, 60))});
            Add(new Label { Text = _message, Transform = new Transform2(new Vector2(0, 60), new Size2(400, 60))});
            Add(Buttons.Text("Cancel", new Point(40, 200), () => {
                Multiplayer.Disconnect();
                Scene.NavigateTo(new LobbyScene());
            }));
            Event.Subscribe<GameConnectionEstablished>(x => Scene.NavigateTo(new RockPaperScissorsGame()), this);
        }
    }
}
