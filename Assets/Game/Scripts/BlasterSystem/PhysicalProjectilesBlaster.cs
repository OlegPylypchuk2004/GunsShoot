using BlasterSystem.Abstractions;
using ProjectileSystem;
using UnityEngine;

namespace BlasterSystem
{
    public class PhysicalProjectilesBlaster : Blaster, IBlasterShotReadonly, IBlasterAmmoAmountReadonly
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

        protected override void LauchProjectile()
        {
            PhysicalProjectileData physicalProjectileData = new PhysicalProjectileData();
            physicalProjectileData.Damage = Config.Damage;
            physicalProjectileData.Direction = GetProjectileDirection();
            physicalProjectileData.Speed = Config.ProjectileSpeed;

            PhysicalProjectile projectile = _projectilesManager.CreatePhysicalProjectile();
            projectile.transform.position = _shootPoint.position;
            projectile.SetRigidbodyPosition(_shootPoint.position);
            projectile.Initialize(physicalProjectileData);
        }

        private Vector3 GetProjectileDirection()
        {
            Vector3 baseDirection = -_shootPoint.right;
            float spreadAngleY = Random.Range(-Config.Spread, Config.Spread);
            float spreadAngleZ = Random.Range(-Config.Spread, Config.Spread);
            Quaternion spreadRotation = Quaternion.Euler(0, spreadAngleZ, spreadAngleY);

            return spreadRotation * baseDirection;
        }
    }
}