using DamageSystem;
using System;
using UnityEngine;

namespace ProjectileSystem
{
    public abstract class PhysicalProjectile : Projectile
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _gravity;
        [SerializeField, Min(1)] private int _collisionsCount;

        private int _currentCollisionsAmount;
        private PhysicalProjectileData _physicalProjectileData;

        public event Action<PhysicalProjectile> Hit;

        private void FixedUpdate()
        {
            _physicalProjectileData.Direction += Vector3.down * _gravity * Time.fixedDeltaTime;
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
                PerformHit(damageable);
            }

            _currentCollisionsAmount++;

            if (_currentCollisionsAmount >= _collisionsCount)
            {
                _currentCollisionsAmount = 0;

                Hit?.Invoke(this);
            }
        }

        public override void Launch(ProjectileData projectileData)
        {
            base.Launch(projectileData);

            _physicalProjectileData = (PhysicalProjectileData)projectileData;
        }

        public void SetRigidbodyPosition(Vector3 position)
        {
            _rigidbody.position = position;
        }

        protected override void PerformHit(IDamageable damageable)
        {
            damageable.TakeDamage(CalculateDamage());
        }
    }
}