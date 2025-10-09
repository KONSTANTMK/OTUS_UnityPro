using System;
using Zenject;
using UnityEngine;
using Core.EventBus;
using Game;

namespace Services
{
    public class TurnService : IInitializable, IDisposable
    {
        public void Initialize()
        {
            Debug.Log("[TurnService] Init");
            EventBus.Subscribe<StartGame>(OnStartGame);
        }

        public void Dispose()
        {
            EventBus.Unsubscribe<StartGame>(OnStartGame);
        }

        private void OnStartGame(StartGame _)
        {
            Debug.Log("[TurnService] StartGame received");
            // позже: запуск первого хода
        }
    }
}