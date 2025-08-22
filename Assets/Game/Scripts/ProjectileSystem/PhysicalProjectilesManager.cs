using BlasterSystem;
using Patterns.ObjectPool;
using System;

namespace ProjectileSystem
{
    public class PhysicalProjectilesManager : ProjectilesManager, IDisposable
    {
        private ObjectPool<PhysicalProjectile> _objectPool;

        public void Dispose()
        {
            foreach (PhysicalProjectile physicalProjectile in _objectPool.Objects)
            {
                physicalProjectile.Hit -= OnProjectileHit;
            }
        }

        public override void Initialize(BlasterConfig blasterConfig)
        {
            _objectPool = new ObjectPool<PhysicalProjectile>((PhysicalProjectile)blasterConfig.Projectile, blasterConfig.AmmoAmount / 10);
        }

        public override Projectile CreateProjectile()
        {
            PhysicalProjectile projectile = _objectPool.Get();
            projectile.Hit += OnProjectileHit;

            return projectile;
        }

        private void OnProjectileHit(PhysicalProjectile projectile)
        {
            projectile.Hit -= OnProjectileHit;
            _objectPool.Release(projectile);
        }
    }
}