using System;
using Atomic.Elements;
using UnityEngine;

namespace Homework.EventBus
{
    [Serializable]
    public sealed class Position
    {
        public Transform transform;
        
        public AtomicVariable<Vector2Int> coordinates;   
    }
}