using Entities;

namespace Homework.EventBus
{
    public class AttackEvent : IEvent
    {
        public readonly IEntity Source;
        public readonly IEntity Target;

        public AttackEvent(IEntity source, IEntity target)
        {
            Source = source;
            Target = target;
        }
    }
}