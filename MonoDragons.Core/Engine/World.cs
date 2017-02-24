using MonoDragons.Core.EventSystem;

namespace MonoDragons.Core.Engine
{
    public static class World
    {
        private static readonly Events _events = new Events();

        public static void PlaySound(string soundName)
        {
            
        }

        public static void PlayMusic(string songName)
        {
            
        }

        public static void NavigateToScene(string sceneView)
        {
            
        }

        public static void Publish<T>(T payload)
        {
            _events.Publish(payload);
        }

        public static void Subscribe<T>(EventSubscription<T> subscription)
        {
            _events.Subscribe(subscription);
        }

        public static void Unsubscribe(object owner)
        {
            _events.Unsubscribe(owner);
        }
    }
}
