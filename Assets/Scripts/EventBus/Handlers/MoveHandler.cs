using System;
using Unity.VisualScripting;

namespace Homework.EventBus
{
    public sealed class MoveHandler : BaseEventHandler<MoveEvent>
    {
        private readonly LevelMap _levelMap;


        public MoveHandler(EventBus eventBus, LevelMap levelMap) : base(eventBus)
        {
            _levelMap = levelMap;
        }

        protected override void OnEventInvoked(MoveEvent evt)
        {
            var coordinates = evt.Entity.Get<CoordinatesComponent>();
            var targetCoordinates = coordinates.Value + evt.Direction;

            _levelMap.Entities.RemoveEntity(coordinates.Value);
            _levelMap.Entities.SetEntity(targetCoordinates, evt.Entity);
            coordinates.Value = targetCoordinates;

            // var position = entity.Get<PositionComponent>();
            // position.Value = _levelMap.Tiles.CoordinatesToPosition(targetCoordinates);
            
            if(_levelMap.Tiles.IsWalkable(targetCoordinates))
            {
                EventBus.RaiseEvent(new DestroyEvent(evt.Entity));
            }
        }
    }
}