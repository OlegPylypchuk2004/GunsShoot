using DamageSystem;
using System;
using System.Collections;
using UnityEngine;

namespace ProjectileSystem
{
    public class RaycastProjectile : Projectile
    {
        [SerializeField, Min(0f)] private float _distance;
        [SerializeField, Min(0f)] protected float _lifeTime;

        public event Action<RaycastProjectile> Expired;

        private Coroutine _lifeTimeCoroutine;

        public override void Launch(ProjectileData projectileData)
        {
            base.Launch(projectileData);

            if (_lifeTimeCoroutine != null)
            {
                StopCoroutine(_lifeTimeCoroutine);
                _lifeTimeCoroutine = null;
            }

            UpdateRotation();
            PerformRaycast();

            _lifeTimeCoroutine = StartCoroutine(CountLifeTime());
        }

        protected override void PerformHit(IDamageable damageable)
        {
            damageable.TakeDamage(CalculateDamage());
        }

        private void PerformRaycast()
        {
            Ray ray = new Ray(transform.position, transform.right);
            RaycastHit[] hits = Physics.RaycastAll(ray, _distance);

            if (hits.Length > 0)
            {
                foreach (RaycastHit hit in hits)
                {
                    if (hit.collider.TryGetComponent(out IDamageable damageable))
                    {
                        PerformHit(damageable);
                    }
                }
            }
        }

        private IEnumerator CountLifeTime()
        {
            yield return new WaitForSeconds(_lifeTime);

            _lifeTimeCoroutine = null;

            Expired?.Invoke(this);
        }

        private void UpdateRotation()
        {
            float angle = Mathf.Atan2(_projectileData.Direction.y, _projectileData.Direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}