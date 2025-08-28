using DamageSystem;
using UnityEngine;

namespace ProjectileSystem
{
    public class Grenade : PhysicalProjectile
    {
        [SerializeField] private int _splashDamage;
        [SerializeField] private float _splashRadius;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, _splashRadius);
        }

        protected override void PerformHit(IDamageable damageable)
        {
            base.PerformHit(damageable);

            PerformSplash(damageable);
        }

        private void PerformSplash(IDamageable ignoreDamageble)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _splashRadius);

            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent(out IDamageable damageable))
                {
                    if (damageable == ignoreDamageble)
                    {
                        continue;
                    }

                    int splashDamage = CalculateSplashDamage(collider.transform.position);

                    if (splashDamage > 0)
                    {
                        damageable.TakeDamage(splashDamage);
                    }
                }
            }
        }

        private int CalculateSplashDamage(Vector3 point)
        {
            float distance = Vector3.Distance(transform.position, point);

            if (distance > _splashRadius)
            {
                return 0;
            }

            float damageFactor = 1.0f - (distance / _splashRadius);
            float finalDamage = _splashDamage * damageFactor;

            return Mathf.FloorToInt(finalDamage);
        }
    }
}