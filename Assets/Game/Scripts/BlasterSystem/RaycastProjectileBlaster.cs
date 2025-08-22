using ProjectileSystem;
using UnityEngine;

namespace BlasterSystem
{
    public class RaycastProjectileBlaster : Blaster
    {
        private void Awake()
        {
            _projectilesManager = new RaycastProjectilesManager();
            _projectilesManager.Initialize(Config);
        }

        private void Start()
        {
            AmmoAmount = Config.AmmoAmount;
        }

        protected override void LauchProjectile()
        {
            RaycastProjectileData projectileData = new RaycastProjectileData();
            projectileData.Damage = Config.Damage;
            projectileData.Direction = GetProjectileDirection();

            RaycastProjectile projectile = (RaycastProjectile)_projectilesManager.CreateProjectile();
            projectile.transform.position = _shootPoint.position;
            projectile.Launch(projectileData);
        }
    }
}