using BlasterSystem.Abstractions;
using BulletSystem;
using System;
using UnityEngine;

namespace BlasterSystem
{
    public class Blaster : MonoBehaviour, IBlasterShotReadonly, IBlasterAmmoAmountReadonly
    {
        [field: SerializeField] public BlasterConfig Config { get; private set; }
        [SerializeField] private Transform _shootPoint;

        private BulletsManager _bulletsManager;
        private BlasterState _state;
        private float _reloadTime;
        private float ShootCooldownTime;
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

        public event Action ShotFired;
        public event Action<int> AmmoAmountChanged;

        private void Awake()
        {
            _bulletsManager = new BulletsManager(Config);
        }

        private void Start()
        {
            AmmoAmount = Config.AmmoAmount;
        }

        private void Update()
        {
            if (_state == BlasterState.ReadyToShoot)
            {
                if (ShootCooldownTime > 0f)
                {
                    ShootCooldownTime -= Time.deltaTime;
                }

                if (ShootCooldownTime <= 0f)
                {
                    if (Input.GetMouseButton(0))
                    {
                        Shoot();
                    }
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
            }
        }

        private void Shoot()
        {
            if (AmmoAmount <= 0)
            {
                StartReload();
                return;
            }

            Bullet bullet = _bulletsManager.CreateBullet();
            bullet.transform.position = _shootPoint.position;
            bullet.SetRigidbodyPosition(_shootPoint.position);
            bullet.Launch(Config.BulletSpeed, Config.Damage, GetBulletDirection());

            AmmoAmount--;
            ShootCooldownTime = Config.ShotCooldown;

            if (AmmoAmount <= 0)
            {
                StartReload();
            }

            ShotFired?.Invoke();
        }

        private Vector3 GetBulletDirection()
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