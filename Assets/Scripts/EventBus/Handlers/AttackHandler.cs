using System.Diagnostics.Tracing;
using Unity.VisualScripting;

namespace Homework.EventBus
{
    public sealed class AttackHandler: BaseEventHandler<AttackEvent>
    { 
        public AttackHandler(EventBus eventBus) : base(eventBus)
        {
        }

        protected override void OnEventInvoked(AttackEvent evt)
        {
            var source = evt.Source;
            
            if (source.TryGet(out StatsComponent stats))
            {
                EventBus.RaiseEvent(new DealDamageEvent(evt.Target, stats.Strength));
            }
            
            if(source.TryGet(out WeaponComponent weaponComponent))
            {
                //Create force direction event
                EventBus.RaiseEvent(new ForceDirectionEvent(source,evt.Target, stats.Strength));

                var effects = weaponComponent.WeaponConfig.Effects;
                
                foreach (var effect in effects)
                {
                    EventBus.RaiseEvent(effect);
                }
            }
        }
    }
}