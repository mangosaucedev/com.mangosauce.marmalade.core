using System;
using System.Collections.Generic;
using System.Text;

namespace Marmalade
{
    public static class Events
    {
        private static Dictionary<Type, EventListenerCollection> listeners = 
            new Dictionary<Type, EventListenerCollection>();

        public static void Fire<T>(T e) where T : Event
        {
            Type type = typeof(T);
            if (listeners.TryGetValue(type, out EventListenerCollection collection))
                collection.Fire(e);
        }

        public static void Listen<T>(
            object listener, EventDelegate<T> action) where T : Event
        {
            Type type = typeof(T);
            EventListenerCollection collection = 
                GetEventListenerCollection(type);
            collection.AddListener(listener, action);
        }

        private static EventListenerCollection GetEventListenerCollection(
            Type type)
        {
            if (!listeners.TryGetValue(
                type, out EventListenerCollection collection))
            {
                collection = new EventListenerCollection();
                listeners[type] = collection;
            }
            return collection;
        }

        public static void StopListening<T>(object listener) where T : Event
        {
            Type type = typeof(T);
            EventListenerCollection collection =
                GetEventListenerCollection(type);
            collection.RemoveListener<T>(listener);
        }
    }
}
