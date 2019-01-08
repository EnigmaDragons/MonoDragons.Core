using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Development;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.UserInterface;

namespace MonoDragons.Core.Network
{
    public sealed class NetworkTestScene : ClickUiScene
    {
        private IReadOnlyCollection<JsonNetGame> _clients = new List<JsonNetGame>();
        private readonly RecentEventDebugLogView _log = new RecentEventDebugLogView {FilterStartsWith = "MonoDragons.Core.UserInterface"};
        private JsonNetGame _host;

        public override void Init()
        {
            const string appId = "MonoDragons Network Test";
            const int port = 44558;
            var networkTypes = new [] { typeof(DemoMessage) };
            _host = JsonNetGame.Host(appId, port, networkTypes);
            Add(_log);
            Add(_host);
            Add(MakeButton("Host", new Point(400, 10), () => _host.Init()));
            Add(MakeButton("Connect", new Point(400, 100), () => AddNewConnection(appId, port, networkTypes)));
            Add(MakeButton("Disconnect", new Point(400, 200), Disconnect));
            Add(MakeButton("Send Client Message", new Point(400, 300), SendClientMessage));
            Add(MakeButton("Send Host Message", new Point(400, 400), SendHostMessage));
            Add(MakeButton("Clear Log", new Point(400, 500), () => _log.Clear()));
            Add(() => _clients.ForEach(x => x.Update(TimeSpan.Zero)));
            Event.Subscribe<DemoMessage>(e => Logger.WriteLine(e.Message), this);
        }

        private void SendHostMessage()
        {
            Logger.WriteLine("-----------------------------------");
            Event.Publish(new DemoMessage { Message = $"Hello From Host"});
        }

        private void Disconnect()
        {
            _clients.Take(1).ForEach(x => x.Dispose());
            _clients = _clients.Skip(1).ToList();
        }

        private void SendClientMessage()
        {
            Logger.WriteLine("-----------------------------------");
            _clients.Take(1).ForEach(c => Event.Publish(new DemoMessage { Message = $"Hello From Client {c.Id}"}));
        }
        
        private void AddNewConnection(string appId, int port, Type[] types)
        {
            _clients = _clients.Append(JsonNetGame.ConnectTo(appId, "127.0.0.1", port, types).Initialized());
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
