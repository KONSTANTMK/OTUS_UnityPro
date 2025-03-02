using UnityEngine;

namespace GameEngine
{
    public class GameContext:MonoBehaviour
    {
        public MoneyStorage MoneyStorage { get => moneyStorage; }
        [SerializeField]
        private MoneyStorage moneyStorage;
    }
}