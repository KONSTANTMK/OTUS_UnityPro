using System;
using UnityEngine;
using Zenject;

namespace Homework.EventBus
{
    public sealed class PlayerInputTask : EventTask
    {
        private  KeyboardInput _input;
        private  IEntity _player;

        private  EventBus _eventBus;

        [Inject]
        public void Construct(KeyboardInput input, 
            PlayerService playerService, EventBus eventBus)
        {
            _input = input;
            _player = playerService.Player;
            _eventBus = eventBus;
        }
        
        protected override void OnStart()
        {
            _input.MovePerformed += OnMovePreformed;
        }

        protected override void OnComplete()
        {
            _input.MovePerformed -= OnMovePreformed;
        }

        private void OnMovePreformed(Vector2Int direction)
        {
            _eventBus.RaiseEvent(new ApplyDirectionEvent(_player, direction));
            Complete();
        }
    }
}