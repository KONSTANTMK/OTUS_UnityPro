using Entities;
using UnityEngine;

namespace Homework.EventBus
{
    public sealed class PlayerService : MonoBehaviour
    {
        public IEntity Player => player;
        
        [SerializeField]
        private MonoEntity player;
    }
}