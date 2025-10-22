using UnityEngine;

namespace Code.OOP
{
    public sealed class CharacterView : MonoBehaviour
    {
        [SerializeField]
        private Transform _firePoint;

        [SerializeField]
        private Rigidbody _bulletPrefab;

        public Transform FirePoint => _firePoint;

        public Rigidbody BulletPrefab => _bulletPrefab;
    }
}
