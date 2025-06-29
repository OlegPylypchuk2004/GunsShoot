using BulletSystem;
using System;
using UnityEngine;

namespace BlasterSystem
{
    public class Blaster : MonoBehaviour
    {
        [field: SerializeField] public BlasterConfig Config { get; private set; }
        [SerializeField] private Transform _shootPoint;

        private BlasterState _state;
        private int _currentAmmo;
        private float _reloadTimer;
        private float _shootCooldownTimer;

        public event Action ShotFired;

        private void Start()
        {
            _currentAmmo = Config.MaxAmmo;
        }

        private void Update()
        {
            if (_state == BlasterState.ReadyToShoot)
            {
                if (_shootCooldownTimer > 0f)
                {
                    _shootCooldownTimer -= Time.deltaTime;
                }

                if (_shootCooldownTimer <= 0f)
                {
                    if (Input.GetMouseButton(0))
                    {
                        Shoot();
                    }
                }
            }
            else if (_state == BlasterState.Reloading)
            {
                _reloadTimer -= Time.deltaTime;

                if (_reloadTimer <= 0f)
                {
                    _state = BlasterState.ReadyToShoot;
                    _currentAmmo = Config.MaxAmmo;
                }
            }
        }

        private void Shoot()
        {
            if (_currentAmmo <= 0)
            {
                StartReload();
                return;
            }

            Bullet bullet = Instantiate(Config.Bullet.Prefab);
            bullet.transform.position = _shootPoint.position;
            bullet.Launch(Config.Bullet.Speed, -_shootPoint.right);

            _currentAmmo--;
            _shootCooldownTimer = Config.ShotCooldown;

            if (_currentAmmo <= 0)
            {
                StartReload();
            }

            ShotFired?.Invoke();
        }

        private void StartReload()
        {
            _state = BlasterState.Reloading;
            _reloadTimer = Config.ReloadDuration;
        }
    }
}