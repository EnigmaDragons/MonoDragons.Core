using System;

namespace MonoDragons.Core.Network
{
    public static class Multiplayer
    {
        private static readonly MustInit<JsonNetGame> Net = new MustInit<JsonNetGame>("jsonNetGame");

        public static void HostGame(string appId, int port, params Type[] types)
            => Net.Init(JsonNetGame.Host(appId, port, types).Initialized());

        public static void JoinGame(string appId, string host, int port, params Type[] types)
            => Net.Init(JsonNetGame.ConnectTo(appId, host, port, types).Initialized());

        public static void Disconnect() => Net.Pop().Dispose();

        public static void Update(TimeSpan delta) => Net.IsInitialized.If(x => x, () => Net.Get().Update(delta));
    }
}
