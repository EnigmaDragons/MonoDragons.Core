using System;

namespace MonoDragons.Core.EventSystem
{
    public class EventSubscription<T>
    {
        public Action<T> OnEvent { get; }
        public object Owner { get; }

        public EventSubscription(Action<T> onEvent, object owner)
        {
            OnEvent = onEvent;
            Owner = owner;
        }
    }
}
