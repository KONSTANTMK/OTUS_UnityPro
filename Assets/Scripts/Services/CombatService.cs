using Zenject;
using UnityEngine;

namespace Services
{
    public class CombatService : IInitializable
    {
        public void Initialize()
        {
            Debug.Log("[CombatService] Init");
            // позже: подписки на события атаки/урона
        }
    }
}