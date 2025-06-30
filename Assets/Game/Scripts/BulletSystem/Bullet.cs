using DamageSystem;
using UnityEngine;

namespace BulletSystem
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;

        private BulletState _state;
        private float _speed;
        private int _damage;
        private Vector3 _direction;

        private void Awake()
        {
            _state = BulletState.Idle;
        }

        private void FixedUpdate()
        {
            if (_state == BulletState.Launched)
            {
                _rigidbody.velocity = _direction * _speed;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_state != BulletState.Launched)
            {
                return;
            }

            _state = BulletState.Idle;

            if (other.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(_damage);
            }

            Destroy(gameObject);
        }

        public void Launch(float speed, int damage, Vector3 direction)
        {
            _speed = speed;
            _damage = damage;
            _direction = direction.normalized;
            _state = BulletState.Launched;
        }
    }
}