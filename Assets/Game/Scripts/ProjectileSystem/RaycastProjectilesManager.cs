using BlasterSystem;
using Patterns.ObjectPool;
using System;

namespace ProjectileSystem
{
    public class RaycastProjectilesManager : ProjectilesManager, IDisposable
    {
        private ObjectPool<RaycastProjectile> _objectPool;

        public void Dispose()
        {

        }

        public override void Initialize(BlasterConfig blasterConfig)
        {
            _objectPool = new ObjectPool<RaycastProjectile>((RaycastProjectile)blasterConfig.Projectile, blasterConfig.AmmoAmount / 5);
        }

        public override Projectile CreateProjectile()
        {
            RaycastProjectile projectile = _objectPool.Get();
            projectile.Expired += OnProjectileExpired;

            return projectile;
        }

        private void OnProjectileExpired(RaycastProjectile projectile)
        {
            projectile.Expired -= OnProjectileExpired;
            _objectPool.Release(projectile);
        }
    }
}