using UnityEngine;
using Core.EventBus;

namespace Game
{
    public class Bootstrapper : MonoBehaviour
    {
        private void Start()
        {
            Debug.Log("[Bootstrapper] Publish StartGame");
            EventBus.Publish(new StartGame());
        }
    }
}