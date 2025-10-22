using System;
using Atomic.Elements;

namespace Homework.EventBus
{
    [Serializable]
    public sealed class Life    
    {
        public AtomicVariable<bool> isDead;

        public AtomicVariable<int> hitPoints = new(1);
        public AtomicVariable<int> maxHitPoints = new(1);
    }
}