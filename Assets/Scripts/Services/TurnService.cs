using System;
using Zenject;
using UnityEngine;
using Core.EventBus;
using Game;
using State;

namespace Services
{
    public class TurnService : IInitializable, IDisposable
    {
        private readonly BoardState _board;

        private int _activeIndex = -1; // для логов/отладки

        public TurnService(BoardState board) => _board = board;

        public void Initialize()
        {
            Debug.Log("[TurnService] Init");
            EventBus.Subscribe<StartGame>(OnStartGame);
            EventBus.Subscribe<EndTurnRequested>(OnEndTurnRequested);
        }

        public void Dispose()
        {
            EventBus.Unsubscribe<StartGame>(OnStartGame);
            EventBus.Unsubscribe<EndTurnRequested>(OnEndTurnRequested);
        }

        private void OnStartGame(StartGame _)
        {
            _board.SetupDefaultTeams();

            var active = _board.NextActive();
            if (active == null) return;

            _activeIndex = active.Index;
            Debug.Log($"[TurnService] TurnStarted -> {active.Name} ({active.Index})");
            EventBus.Publish(new TurnStarted { ActiveIndex = active.Index });
        }

        private void OnEndTurnRequested(EndTurnRequested _)
        {
            if (_activeIndex < 0) return;

            // Закрываем текущий ход
            EventBus.Publish(new TurnEnded { ActiveIndex = _activeIndex });

            // Подаём следующего
            var next = _board.NextActive();
            if (next == null) return;

            _activeIndex = next.Index;
            Debug.Log($"[TurnService] TurnStarted -> {next.Name} ({next.Index})");
            EventBus.Publish(new TurnStarted { ActiveIndex = next.Index });
        }
    }
}