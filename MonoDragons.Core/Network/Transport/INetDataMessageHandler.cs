namespace MonoDragons.Core.Network
{
    public interface INetDataMessageHandler
    {
        void Handle(long connectionId, string body);
    }
}