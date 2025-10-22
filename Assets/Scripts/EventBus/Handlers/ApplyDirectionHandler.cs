using Zenject;

namespace Homework.EventBus
{
    public sealed class ApplyDirectionHandler : BaseEventHandler<ApplyDirectionEvent>
    {
        private readonly LevelMap _levelMap;

        public ApplyDirectionHandler(EventBus eventBus, LevelMap levelMap) : base(eventBus)
        {
            _levelMap = levelMap;
        }

        protected override void OnEventInvoked(ApplyDirectionEvent evt)
        {
            var coordinates = evt.Entity.Get<CoordinatesComponent>();
            var targetCoordinates = coordinates.Value + evt.Direction;

            if (_levelMap.Entities.HasEntity(targetCoordinates))
            {
                var target = _levelMap.Entities.GetEntity(targetCoordinates);
                EventBus.RaiseEvent(new AttackEvent(evt.Entity, target));
                return;
            }

            if (_levelMap.Tiles.IsWalkable(targetCoordinates))
            {
                EventBus.RaiseEvent(new MoveEvent(evt.Entity, evt.Direction));
            }
        }
    }
}