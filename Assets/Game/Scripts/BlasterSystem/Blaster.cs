using BlasterSystem.Abstractions;
using BulletSystem;
using System;
using UnityEngine;

namespace BlasterSystem
{
    public class Blaster : MonoBehaviour, IBlasterShotReadonly
    {
        [field: SerializeField] public BlasterConfig Config { get; private set; }
        [SerializeField] private Transform _shootPoint;

        private BulletsManager _bulletsManager;

        public BlasterState State { get; private set; }
        public int Ammo { get; private set; }
        public float ReloadTime { get; private set; }
        public float ShootCooldownTime { get; private set; }

        public event Action ShotFired;

        private void Awake()
        {
            _bulletsManager = new BulletsManager(Config);
        }

        private void Start()
        {
            Ammo = Config.Ammo;
        }

        private void Update()
        {
            if (State == BlasterState.ReadyToShoot)
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
            else if (State == BlasterState.Reloading)
            {
                ReloadTime -= Time.deltaTime;

                if (ReloadTime <= 0f)
                {
                    State = BlasterState.ReadyToShoot;
                    Ammo = Config.Ammo;
                }
            }
        }

        private void Shoot()
        {
            if (Ammo <= 0)
            {
                StartReload();
                return;
            }

            Bullet bullet = _bulletsManager.CreateBullet();
            bullet.transform.position = _shootPoint.position;
            bullet.SetRigidbodyPosition(_shootPoint.position);
            bullet.Launch(Config.BulletSpeed, Config.Damage, -_shootPoint.right);

            Ammo--;
            ShootCooldownTime = Config.ShotCooldown;

            if (Ammo <= 0)
            {
                StartReload();
            }

            ShotFired?.Invoke();
        }

        private void StartReload()
        {
            State = BlasterState.Reloading;
            ReloadTime = Config.ReloadDuration;
        }
    }
}