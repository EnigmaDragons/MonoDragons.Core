using System;
using System.Collections.Generic;
using System.Linq;
using Lidgren.Network;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;

namespace MonoDragons.Core.Network
{
    public sealed class NetGame : IDisposable, IInitializable, IAutomaton
    {
        private readonly INetDataMessageHandler _handler;
        private readonly Action<NetPeer> _connect;
        internal readonly NetRole Role;
        internal readonly NetPeer Net;

        public static NetGame CreateHost(string appId, int port, INetDataMessageHandler handler)
        {
            return new NetGame(NetRole.Host, new NetPeerConfiguration(appId) { Port = port }, handler, connect: net =>
            {
                net.Start();
                Event.Publish(new GameServerStarted { Port = port });
            });
        }

        public static NetGame CreateClient(string appId, string host, int port, INetDataMessageHandler handler)
        {
            return new NetGame(NetRole.Client, new NetPeerConfiguration(appId), handler, connect: net =>
            {
                net.Start();
                net.Connect(host, port);
                Event.Publish(new GameConnectionEstablished { Address = $"{host}:{port}"});
            });
        }
        
        private NetGame(NetRole role, NetPeerConfiguration config, INetDataMessageHandler handler, Action<NetPeer> connect)
        {
            Role = role;
            _handler = handler;
            _connect = connect;
            Net = new NetServer(config);
        }

        public void Init() => _connect(Net);
        public void Dispose() => Net.Shutdown("Connection Has Been Terminated");

        public void Update(TimeSpan delta)
        {
            NetIncomingMessage msg;
            while ((msg = Net.ReadMessage()) != null)
            {
                switch (msg.MessageType)
                {
                    case NetIncomingMessageType.VerboseDebugMessage:
                    case NetIncomingMessageType.DebugMessage:
                    case NetIncomingMessageType.WarningMessage:
                    case NetIncomingMessageType.ErrorMessage:
                        Log(msg.ReadString());
                        break;
                    case NetIncomingMessageType.StatusChanged:
                        OnStatusChanged(msg);
                        break;
                    case NetIncomingMessageType.Data:
                        OnData(msg);
                        break;
                    default:
                        Log($"Unknown Message Type: {msg.MessageType}");
                        break;
                }
                Net.Recycle(msg);
            }
        }

        private void OnData(NetIncomingMessage msg)
        {
            var msgBody = msg.ReadString();
            RelayMessageToOtherClients(msg, msgBody);
            _handler.Handle(Net.UniqueIdentifier, msgBody);
        }

        private void RelayMessageToOtherClients(NetIncomingMessage msg, string msgBody)
        {
            var otherClients = OtherClients(msg);
            if (otherClients.Any())
                Net.SendMessage(Net.CreateMessage(msgBody), otherClients, NetDeliveryMethod.ReliableUnordered, 0);
        }

        private List<NetConnection> OtherClients(NetIncomingMessage msg)
            => Net.Connections.Where(x => x.RemoteUniqueIdentifier != msg.SenderConnection.RemoteUniqueIdentifier).ToList();

        private void OnStatusChanged(NetIncomingMessage msg)
        {
            var status = (NetConnectionStatus)msg.ReadByte();
            var msgBody = msg.ReadString();
            Log($"Conn: {msg.SenderEndPoint} {status} - {msgBody}");
            if (status.Equals(NetConnectionStatus.Connected))
                Event.Publish(new GameConnectionEstablished { Address = msg.SenderEndPoint.ToString(), NumActiveConnections = Net.ConnectionsCount });
        }

        private void Log(string message) => Logger.WriteLine($"{Role}: {message}");
    }
}
