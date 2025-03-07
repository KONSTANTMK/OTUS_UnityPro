using UnityEngine;

namespace GameEngine
{
    public class MoneyService: MonoBehaviour
    {
        public int Money { get => money;}
        
        [SerializeField]
        private int money;

        [SerializeField]
        public void SetupMoney(int money)
        {
            this.money = money;
        }
    }
}