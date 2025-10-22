using System;
using UnityEngine;

namespace Homework.EventBus
{
    public sealed class KeyboardInput : MonoBehaviour
    {
        public event Action<Vector2Int> MovePerformed;
        
        private void Update()
        {
            var movement = new Vector2Int();
            
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                movement.y += 1;
            }
            
            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                movement.y -= 1;
            }
            
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                movement.x += 1;
            }
            
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                movement.x -= 1;
            }
            
            if (movement != Vector2Int.zero)
            {
                MovePerformed?.Invoke(movement);
            }
        }
    }
}