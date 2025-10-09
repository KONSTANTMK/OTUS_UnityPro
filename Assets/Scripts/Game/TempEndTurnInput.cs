using UnityEngine;
using Core.EventBus;

namespace Game
{
    public class TempEndTurnInput : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                EventBus.Publish(new EndTurnRequested());
        }
    }
}