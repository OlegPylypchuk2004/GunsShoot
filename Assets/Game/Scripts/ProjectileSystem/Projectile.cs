using DamageSystem;
using UnityEngine;

namespace ProjectileSystem
{
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField, Range(0, 100)] private int _damageSpreadPercent;

        protected ProjectileData _projectileData;

        public virtual void Launch(ProjectileData projectileData)
        {
            _projectileData = projectileData;

            OnLaunched();
        }

        protected virtual void OnLaunched()
        {

        }

        protected int CalculateDamage()
        {
            int maxSpread = Mathf.RoundToInt(_projectileData.Damage * _damageSpreadPercent / 100f);
            int randomOffset = Random.Range(-maxSpread, maxSpread + 1);

            return _projectileData.Damage + randomOffset;
        }

        protected abstract void PerformHit(IDamageable damageable);
    }
}