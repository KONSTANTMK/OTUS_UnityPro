using System;
using Atomic.Elements;

namespace Homework.EventBus
{
    [Serializable]
    public sealed class Stats
    {
        public AtomicVariable<int> strength = new(1);
    }
}