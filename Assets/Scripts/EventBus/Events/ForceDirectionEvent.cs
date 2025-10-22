namespace Homework.EventBus
{
    public class ForceDirectionEvent : IEvent
    {
        public readonly IEntity Source;
        public readonly IEntity Target;
        public int Force;

        public ForceDirectionEvent(IEntity source, IEntity target,int force)
        {
            Source = source;
            Target = target;
            Force = force;
        }
    }
}