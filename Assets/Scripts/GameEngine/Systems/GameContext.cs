using UnityEngine;
using Zenject;

namespace GameEngine
{
    public class GameContext
    {
        public MoneyStorage MoneyStorage { get; private set; }

        [Inject]
        public void Construct(MoneyStorage moneyStorage)
        {
            MoneyStorage = moneyStorage;
        }
    }
}