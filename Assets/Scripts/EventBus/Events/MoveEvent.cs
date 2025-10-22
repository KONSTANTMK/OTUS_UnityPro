using UnityEngine;

namespace Homework.EventBus
{
    public class MoveEvent : IEvent
    {
        public readonly IEntity Entity;
        public readonly Vector2Int Direction;

        public MoveEvent(IEntity entity, Vector2Int direction)
        {
            Entity = entity;
            Direction = direction;
        }
    }
}