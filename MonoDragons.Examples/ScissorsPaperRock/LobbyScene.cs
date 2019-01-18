using System;
using Microsoft.Xna.Framework;
using MonoDragons.Core;
using MonoDragons.Core.Development;
using MonoDragons.Core.Network;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.UserInterface;

namespace MonoDragons.Examples.ScissorsPaperRock
{
    public sealed class LobbyScene : ClickUiScene
    {
        private const string AppId = "Scissors Paper Rock Example";
        private const int Port = 44559;
        private static readonly Type[] NetTypes = new [] { typeof(SelectionMade) };
        private readonly Label _hostEndpoint = new Label { Transform = new Transform2(new Vector2(0, 400), new Size2(400, 60))};
        
        public override void Init()
        {
            Add(Buttons.Text("Host", new Point(100, 160), BeginHostingGame));
            Add(Buttons.Text("Connect", new Point(100, 60), ConnectToGame));
            Add(new Label { Text = "Connect To:", Transform = new Transform2(new Vector2(0, 0), new Size2(400, 60))});
            Add(_hostEndpoint);
            Add(new KeyboardTyping("127.0.0.1").OutputTo(x => _hostEndpoint.Text = x));
        }

        private void BeginHostingGame()
        {
            Multiplayer.HostGame(AppId, Port, NetTypes);
            Scene.NavigateTo(new WaitingForConnectionScene($"Hosting on Port: {Port}"));
        }

        private void ConnectToGame()
        {
            Multiplayer.JoinGame(AppId, _hostEndpoint.Text, Port, NetTypes);
            Scene.NavigateTo(new WaitingForConnectionScene($"Connecting to host... {_hostEndpoint.Text}:{Port}"));
        }
    }
}
