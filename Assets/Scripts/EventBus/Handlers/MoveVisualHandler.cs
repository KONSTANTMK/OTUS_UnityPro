using System;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine.Networking;

namespace Homework.EventBus
{
    public sealed class MoveVisualHandler : BaseEventHandler<MoveEvent>
    {
        private readonly VisualPipeline _visualPipeline;
        private readonly LevelMap _levelMap;


        public MoveVisualHandler(EventBus eventBus, VisualPipeline visualPipeline, LevelMap levelMap) : base(eventBus)
        {
            _visualPipeline = visualPipeline;
            _levelMap = levelMap;
        }

        protected override void OnEventInvoked(MoveEvent evt)
        {
            var coordinates = evt.Entity.Get<CoordinatesComponent>();
            var targetCoordinates = _levelMap.Tiles.CoordinatesToPosition(Component.Value);
            
            /*var position = evt.Entity.Get<PositionComponent>();
            position.Value = _levelMap.Tiles.CoordinatesToPosition(targetCoordinates);*/
            
           _visualPipeline.AddTask(new MoveVisualTask(evt.Entity, targetCoordinates));
        }
        
    }
}