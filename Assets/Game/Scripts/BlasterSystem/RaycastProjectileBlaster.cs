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

        protected override void LauchProjectiles()
        {
            Vector3[] projectilesDirections = GetProjectilesDirections();

            for (int i = 0; i < projectilesDirections.Length; i++)
            {
                RaycastProjectileData projectileData = new RaycastProjectileData();
                projectileData.Damage = Config.Damage;
                projectileData.Direction = projectilesDirections[i];

                RaycastProjectile projectile = (RaycastProjectile)_projectilesManager.CreateProjectile();
                projectile.transform.position = _shootPoints[i].position;
                projectile.Launch(projectileData);
            }
        }
    }
}