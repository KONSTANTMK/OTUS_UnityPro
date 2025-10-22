using UnityEngine;

namespace Code.OOP
{
    public sealed class CharacterFireController
    {
        private readonly CharacterData _data;
        private readonly CharacterView _view;

        public CharacterFireController(CharacterData data, CharacterView view)
        {
            _data = data;
            _view = view;
        }

        public void OnUpdate()
        {
            if (IsFirePressDown() == false)
            {
                return;
            }

            Rigidbody viewBulletPrefab = _view.BulletPrefab;
            Rigidbody instantiate = Object.Instantiate(viewBulletPrefab, _view.FirePoint.position, _view.FirePoint.rotation);
            instantiate.AddForce(_view.FirePoint.up * _data.BulletSpeed, ForceMode.Impulse);
        }
        
        private bool IsFirePressDown()
        { 
            return Input.GetKeyDown(KeyCode.Space);
        }
    }
}
