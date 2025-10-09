using System;
using System.Collections.Generic;

namespace Core.EventBus
{
    public static class EventBus
    {
        private static readonly Dictionary<Type, Delegate> _map = new();

        public static void Subscribe<T>(Action<T> handler) where T : IGameEvent
        {
            var t = typeof(T);
            _map[t] = _map.TryGetValue(t, out var cur) ? Delegate.Combine(cur, handler) : handler;
        }

        public static void Unsubscribe<T>(Action<T> handler) where T : IGameEvent
        {
            var t = typeof(T);
            if (!_map.TryGetValue(t, out var cur)) return;
            var next = Delegate.Remove(cur, handler);
            if (next == null) _map.Remove(t);
            else _map[t] = next;
        }

        public static void Publish<T>(T evt) where T : IGameEvent
        {
            if (_map.TryGetValue(typeof(T), out var del))
                (del as Action<T>)?.Invoke(evt);
        }
    }
}