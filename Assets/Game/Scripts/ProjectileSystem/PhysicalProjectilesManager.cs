using BlasterSystem;
using Patterns.ObjectPool;
using System;

namespace ProjectileSystem
{
    public class PhysicalProjectilesManager : IDisposable
    {
        private ObjectPool<PhysicalProjectile> _objectPool;

        public PhysicalProjectilesManager(BlasterConfig blasterConfig)
        {
            _objectPool = new ObjectPool<PhysicalProjectile>((PhysicalProjectile)blasterConfig.Projectile, blasterConfig.AmmoAmount / 10);
        }

        public void Dispose()
        {
            foreach (PhysicalProjectile physicalProjectile in _objectPool.Objects)
            {
                physicalProjectile.Hit -= OnPhysicalProjectileHit;
            }
        }

        public PhysicalProjectile CreatePhysicalProjectile()
        {
            PhysicalProjectile physicalProjectile = _objectPool.Get();
            physicalProjectile.Hit += OnPhysicalProjectileHit;

            return physicalProjectile;
        }

        private void OnPhysicalProjectileHit(PhysicalProjectile physicalProjectile)
        {
            physicalProjectile.Hit -= OnPhysicalProjectileHit;
            _objectPool.Release(physicalProjectile);
        }
    }
}