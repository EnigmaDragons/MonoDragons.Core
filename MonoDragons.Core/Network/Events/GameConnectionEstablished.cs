namespace MonoDragons.Core.Network
{
    public sealed class GameConnectionEstablished
    {
        public string Address { get; set; }
        public int NumActiveConnections { get; set; }
    }
}
