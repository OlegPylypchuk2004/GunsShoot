using DamageSystem;
using System;
using UnityEngine;

namespace BulletSystem
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField, Range(0, 100)] private int _damageSpreadPercent;
        [SerializeField] private Rigidbody _rigidbody;

        private float _speed;
        private int _damage;
        private Vector3 _direction;

        public event Action<Bullet> Hit;

        private void FixedUpdate()
        {
            _rigidbody.velocity = _direction * _speed;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(CalculateDamage());
            }

            Hit?.Invoke(this);
        }

        public void Launch(float speed, int damage, Vector3 direction)
        {
            _speed = speed;
            _damage = damage;
            _direction = direction.normalized;
        }

        public void SetRigidbodyPosition(Vector3 position)
        {
            _rigidbody.position = position;
        }

        private int CalculateDamage()
        {
            int maxSpread = Mathf.RoundToInt(_damage * _damageSpreadPercent / 100f);
            int randomOffset = UnityEngine.Random.Range(-maxSpread, maxSpread + 1);

            return _damage + randomOffset;
        }
    }
}