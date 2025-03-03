using UnityEngine;
using Zenject;

namespace GameEngine
{
    public class GameContext:MonoBehaviour
    {
        public MoneyStorage MoneyStorage { get => moneyStorage; }
        private MoneyStorage moneyStorage;
        
            
        [Inject]
        public void Construct(MoneyStorage moneyStorage)
        {
            this.moneyStorage = moneyStorage;
        }
    }
}