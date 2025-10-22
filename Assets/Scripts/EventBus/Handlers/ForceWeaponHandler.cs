namespace Homework.EventBus
{
    public class ForceWeaponHandler : BaseEventHandler<ForceWeaponEffect>
    {
        private LevelMap _levelMap;
        
        public ForceWeaponHandler(EventBus eventBus, LevelMap levelMap) : base(eventBus)
        {
            _levelMap = levelMap;
        }

        protected override void OnEventInvoked(ForceWeaponEffect evt)
        {
            EventBus.RaiseEvent(new ForceDirectionEvent(evt.Source, evt.Target,1));
        }
    }
}