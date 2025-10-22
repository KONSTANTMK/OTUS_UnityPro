namespace Homework.EventBus
{
    public class DestroyEvent : IEvent
    {
        public readonly IEntity Entity;

        public DestroyEvent(IEntity entity)
        {
            Entity = entity;
        }
    }
}