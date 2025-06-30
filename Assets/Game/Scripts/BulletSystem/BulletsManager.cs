using BlasterSystem;
using Patterns.ObjectPool;
using System;

namespace BulletSystem
{
    public class BulletsManager : IDisposable
    {
        private ObjectPool<Bullet> _objectPool;

        public BulletsManager(BlasterConfig blasterConfig)
        {
            _objectPool = new ObjectPool<Bullet>(blasterConfig.Bullet.Prefab, blasterConfig.Ammo);
        }

        public void Dispose()
        {
            foreach (Bullet bullet in _objectPool.Objects)
            {
                bullet.Hit -= OnBulletHit;
            }
        }

        public Bullet CreateBullet()
        {
            Bullet bullet = _objectPool.Get();
            bullet.Hit += OnBulletHit;

            return bullet;
        }

        private void OnBulletHit(Bullet bullet)
        {
            bullet.Hit -= OnBulletHit;
            _objectPool.Release(bullet);
        }
    }
}