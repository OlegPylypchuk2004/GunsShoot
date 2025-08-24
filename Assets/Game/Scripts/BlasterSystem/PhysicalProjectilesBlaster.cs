using ProjectileSystem;
using UnityEngine;

namespace BlasterSystem
{
    public class PhysicalProjectilesBlaster : Blaster
    {
        private void Awake()
        {
            _projectilesManager = new PhysicalProjectilesManager();
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
                PhysicalProjectileData projectileData = new PhysicalProjectileData();
                projectileData.Damage = Config.Damage;
                projectileData.Direction = projectilesDirections[i];
                projectileData.Speed = Config.ProjectileSpeed;

                PhysicalProjectile projectile = (PhysicalProjectile)_projectilesManager.CreateProjectile();
                projectile.transform.position = _shootPoints[i].position;
                projectile.SetRigidbodyPosition(_shootPoints[i].position);
                projectile.Launch(projectileData);
            }
        }
    }
}