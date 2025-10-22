namespace Homework.EventBus
{
    public sealed class DealDamageHandler: BaseEventHandler<DealDamageEvent>
    {
        public DealDamageHandler(EventBus eventBus) : base(eventBus)
        {
        
        }

        protected override void OnEventInvoked(DealDamageEvent evt)
        {
            var entity = evt.Entity;
            var damage = evt.Damage;
            
            if (!entity.TryGet(out HitPointsComponent hitPoints))
            {
                return;
            }
            
            hitPoints.Value -= damage;

            if (hitPoints.Value <= 0)
            {
                EventBus.RaiseEvent(new DestroyEvent(entity));
            }
        }
    }
}