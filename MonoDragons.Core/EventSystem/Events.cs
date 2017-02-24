using System;
using System.Collections.Generic;

namespace SecurityConsultantCore.EventSystem
{
    public class Events : IEvents
    {
        Dictionary<Type, List<object>> _subscriptions = new Dictionary<Type, List<object>>();

        public void Publish<T>(T payload)
        {
            var eventType = typeof(T);
            if (_subscriptions.ContainsKey(eventType))
            {
                foreach (var e in _subscriptions[eventType])
                {
                    ((Action<T>)e)(payload);
                }
            }
        }

        public void Subscribe<T>(Action<T> onEvent)
        {
            var eventType = typeof(T);
            if (!_subscriptions.ContainsKey(eventType))
                _subscriptions[eventType] = new List<object>();
            _subscriptions[eventType].Add(onEvent);
        }
    }
}
