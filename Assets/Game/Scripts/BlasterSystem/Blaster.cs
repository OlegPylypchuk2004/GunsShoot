using BulletSystem;
using UnityEngine;

namespace BlasterSystem
{
    public class Blaster : MonoBehaviour
    {
        [SerializeField] private BlasterConfig _config;
        [SerializeField] private Transform _shootPoint;

        private BlasterState _state;
        private int _currentAmmo;
        private float _reloadTimer;
        private float _shootCooldownTimer;

        private void Start()
        {
            _currentAmmo = _config.MaxAmmo;
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
                    _currentAmmo = _config.MaxAmmo;
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

            Bullet bullet = Instantiate(_config.Bullet.Prefab);
            bullet.transform.position = _shootPoint.position;
            bullet.Launch(_config.Bullet.Speed, -_shootPoint.right);

            _currentAmmo--;
            _shootCooldownTimer = _config.ShotCooldown;

            if (_currentAmmo <= 0)
            {
                StartReload();
            }
        }

        private void StartReload()
        {
            _state = BlasterState.Reloading;
            _reloadTimer = _config.ReloadDuration;
        }
    }
}