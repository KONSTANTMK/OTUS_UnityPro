using UnityEngine;

namespace Code.OOP
{
    public sealed class CharacterData
    {
        public Vector3 Position => _position;
        public float BulletSpeed => _moveSpeed * 10;

        private Vector3 _position; 
        private Quaternion _rotation;
        private readonly float _moveSpeed;

        public CharacterData(float moveSpeed)
        {
            _moveSpeed = moveSpeed;
        }

        public void Move(Vector3 moveDirection)
        {
            _position = Position + moveDirection * (_moveSpeed * Time.deltaTime);
        }
    }
}
