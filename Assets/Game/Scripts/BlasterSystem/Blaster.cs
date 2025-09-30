using BlasterSystem.Abstractions;
using ProjectileSystem;
using System;
using UnityEngine;

namespace BlasterSystem
{
    public abstract class Blaster : MonoBehaviour, IBlasterShotReadonly, IBlasterAmmoAmountReadonly
    {
        [field: SerializeField] public BlasterConfig Config { get; private set; }
        [SerializeField] protected Transform[] _shootPoints;

        protected ProjectilesManager _projectilesManager;
        private BlasterState _state;
        private float _reloadTime;
        private float _shootCooldownTime;
        private int _ammoAmount;

        public event Action ShotFired;
        public event Action<int> AmmoAmountChanged;
        public event Action<BlasterState> StateChanged;
        public event Action<float, float> ReloadTimeChanged;

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
            private set
            {
                _state = value;

                StateChanged?.Invoke(State);
            }
        }

        protected virtual void Update()
        {
            if (State == BlasterState.ReadyToShoot)
            {
                if (_shootCooldownTime > 0f)
                {
                    _shootCooldownTime -= Time.deltaTime;
                }
            }
            else if (State == BlasterState.Reloading)
            {
                _reloadTime -= Time.deltaTime;

                if (_reloadTime <= 0f)
                {
                    State = BlasterState.ReadyToShoot;
                    AmmoAmount = Config.AmmoAmount;
                }

                ReloadTimeChanged?.Invoke(_reloadTime, Config.ReloadDuration);
            }
        }

        private void OnDrawGizmos()
        {
            if (_shootPoints == null || _shootPoints.Length == 0)
            {
                return;
            }

            Gizmos.color = Color.white;

            foreach (Transform shootPoint in _shootPoints)
            {
                Gizmos.DrawLine(shootPoint.position, shootPoint.position + shootPoint.right * -1f);
            }
        }

        public void Shoot()
        {
            if (State != BlasterState.ReadyToShoot || _shootCooldownTime > 0f)
            {
                return;
            }

            if (AmmoAmount <= 0)
            {
                StartReload();

                return;
            }

            LauchProjectiles();
            AmmoAmount--;
            _shootCooldownTime = Config.ShotCooldown;

            if (AmmoAmount <= 0)
            {
                StartReload();
            }

            ShotFired?.Invoke();
        }

        private void StartReload()
        {
            State = BlasterState.Reloading;
            _reloadTime = Config.ReloadDuration;
        }

        protected Vector3[] GetProjectilesDirections()
        {
            Vector3[] directions = new Vector3[_shootPoints.Length];

            for (int i = 0; i < _shootPoints.Length; i++)
            {
                Vector3 baseDirection = -_shootPoints[i].right;
                float spreadAngleY = UnityEngine.Random.Range(-Config.Spread, Config.Spread);
                float spreadAngleZ = UnityEngine.Random.Range(-Config.Spread, Config.Spread);
                Quaternion spreadRotation = Quaternion.Euler(0, spreadAngleZ, spreadAngleY);

                directions[i] = spreadRotation * baseDirection;
            }

            return directions;
        }

        protected abstract void LauchProjectiles();
    }
}