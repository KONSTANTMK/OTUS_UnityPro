using UnityEngine;

namespace Code.OOP
{
    public sealed class CharacterMoveController
    {
        private readonly CharacterData _data;
        private readonly CharacterView _view;

        public CharacterMoveController(CharacterData data, CharacterView view)
        {
            _data = data;
            _view = view;
        }

        public void OnUpdate()
        {
            _data.Move(GetDirection());
            _view.transform.position = _data.Position;
        }
        
        private Vector3 GetDirection()
        {
            Vector3 direction = Vector3.zero;

            if (Input.GetKey(KeyCode.W))
            {
                direction.z = 1;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                direction.z = -1;
            }

            if (Input.GetKey(KeyCode.A))
            {
                direction.x = -1;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                direction.x = 1;
            }

            return direction;
        }
    }
}