using DamageSystem;
using System;
using UnityEngine;

namespace ProjectileSystem
{
    public abstract class PhysicalProjectile : Projectile
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField, Min(1)] private int _collisionsCount;

        private int _currentCollisionsAmount;
        private PhysicalProjectileData _physicalProjectileData;

        public event Action<PhysicalProjectile> Hit;

        private void FixedUpdate()
        {
            _rigidbody.velocity = _physicalProjectileData.Direction * _physicalProjectileData.Speed;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("MapBorder"))
            {
                _currentCollisionsAmount = 0;

                Hit?.Invoke(this);

                return;
            }

            if (other.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(CalculateDamage());
            }

            _currentCollisionsAmount++;

            if (_currentCollisionsAmount >= _collisionsCount)
            {
                _currentCollisionsAmount = 0;

                Hit?.Invoke(this);
            }
        }

        public override void Initialize(ProjectileData projectileData)
        {
            base.Initialize(projectileData);

            _physicalProjectileData = (PhysicalProjectileData)projectileData;
        }

        public void SetRigidbodyPosition(Vector3 position)
        {
            _rigidbody.position = position;
        }
    }
}