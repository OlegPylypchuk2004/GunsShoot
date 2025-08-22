using BlasterSystem.Abstractions;
using ProjectileSystem;
using System;
using UnityEngine;

namespace BlasterSystem
{
    public class Blaster : MonoBehaviour, IBlasterShotReadonly, IBlasterAmmoAmountReadonly
    {
        [field: SerializeField] public BlasterConfig Config { get; private set; }
        [SerializeField] private Transform _shootPoint;

        private PhysicalProjectilesManager _bulletsManager;
        private BlasterState _state;
        private float _reloadTime;
        private float _shootCooldownTime;
        private int _ammoAmount;

        public int AmmoAmount
        {
            get => _ammoAmount;
            set
            {
                if (value < 0)
                {
                    return;
                }

                _ammoAmount = value;

                AmmoAmountChanged?.Invoke(_ammoAmount);
            }
        }

        public int MaxAmmoAmount
        {
            get => Config.AmmoAmount;
        }

        public BlasterState State
        {
            get => _state;
            set
            {
                _state = value;

                StateChanged?.Invoke(State);
            }
        }

        public event Action ShotFired;
        public event Action<int> AmmoAmountChanged;
        public event Action<BlasterState> StateChanged;
        public event Action<float, float> ReloadTimeChanged;

        private void Awake()
        {
            _bulletsManager = new PhysicalProjectilesManager(Config);
        }

        private void Start()
        {
            AmmoAmount = Config.AmmoAmount;
        }

        private void Update()
        {
            if (_state == BlasterState.ReadyToShoot)
            {
                if (_shootCooldownTime > 0f)
                {
                    _shootCooldownTime -= Time.deltaTime;
                }
            }
            else if (_state == BlasterState.Reloading)
            {
                _reloadTime -= Time.deltaTime;

                if (_reloadTime <= 0f)
                {
                    _state = BlasterState.ReadyToShoot;
                    AmmoAmount = Config.AmmoAmount;
                }

                ReloadTimeChanged?.Invoke(_reloadTime, Config.ReloadDuration);
            }
        }

        public void Shoot()
        {
            if (_state != BlasterState.ReadyToShoot)
            {
                return;
            }

            if (_shootCooldownTime > 0f)
            {
                return;
            }

            if (AmmoAmount <= 0)
            {
                StartReload();

                return;
            }

            PhysicalProjectileData physicalProjectileData = new PhysicalProjectileData();
            physicalProjectileData.Damage = Config.Damage;
            physicalProjectileData.Direction = GetProjectileDirection();
            physicalProjectileData.Speed = Config.BulletSpeed;

            PhysicalProjectile projectile = _bulletsManager.CreatePhysicalProjectile();
            projectile.transform.position = _shootPoint.position;
            projectile.SetRigidbodyPosition(_shootPoint.position);
            projectile.Initialize(physicalProjectileData);

            AmmoAmount--;
            _shootCooldownTime = Config.ShotCooldown;

            if (AmmoAmount <= 0)
            {
                StartReload();
            }

            ShotFired?.Invoke();
        }

        private Vector3 GetProjectileDirection()
        {
            Vector3 baseDirection = -_shootPoint.right;
            float spreadAngleY = UnityEngine.Random.Range(-Config.Spread, Config.Spread);
            float spreadAngleZ = UnityEngine.Random.Range(-Config.Spread, Config.Spread);
            Quaternion spreadRotation = Quaternion.Euler(0, spreadAngleZ, spreadAngleY);

            return spreadRotation * baseDirection;
        }
        private void StartReload()
        {
            _state = BlasterState.Reloading;
            _reloadTime = Config.ReloadDuration;
        }
    }
}