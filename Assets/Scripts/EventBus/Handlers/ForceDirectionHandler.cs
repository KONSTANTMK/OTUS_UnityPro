using System;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

namespace Homework.EventBus
{
    public class ForceDirectionHandler : BaseEventHandler<ForceDirectionEvent>
    {
        private LevelMap _levelMap;
        
        public ForceDirectionHandler(EventBus eventBus, LevelMap levelMap) : base(eventBus)
        {
            _levelMap = levelMap;
        }

        protected override void OnEventInvoked(ForceDirectionEvent evt)
        {
            var sourceCoordinates = evt.Source.Get<CoordinatesComponent>().Value;
            var targetCoordinates = evt.Target.Get<CoordinatesComponent>().Value;

            var direction = targetCoordinates- sourceCoordinates;
            var targetPosition = targetCoordinates + direction;
            
            if (_levelMap.Entities.HasEntity(targetPosition))
            {
                EventBus.RaiseEvent(new DealDamageEvent(evt.Source, 1));
                EventBus.RaiseEvent(new DealDamageEvent(evt.Target, 1));
                return;
            }
            
            EventBus.RaiseEvent(new MoveEvent(evt.Target, direction));
        }
    }
}