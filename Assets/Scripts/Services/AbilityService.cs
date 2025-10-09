using Zenject;
using UnityEngine;

namespace Services
{
    public class AbilityService : IInitializable
    {
        public void Initialize()
        {
            Debug.Log("[AbilityService] Init");
            // позже: подписки на события пассивок/эффектов
        }
    }
}