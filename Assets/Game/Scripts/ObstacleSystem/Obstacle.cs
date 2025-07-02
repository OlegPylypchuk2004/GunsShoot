using DamageSystem;
using System;
using UnityEngine;

namespace ObstacleSystem
{
    public class Obstacle : MonoBehaviour, IDamageable
    {
        [field: SerializeField] public int MaxHealth { get; private set; }

        private int _health;

        public int Health
        {
            get => _health;
            set
            {
                if (_health != value)
                {
                    _health = value;

                    HealthChanged?.Invoke(_health);
                }
            }
        }

        public event Action<int> HealthChanged;
        public event Action<Obstacle> Destroyed;

        private void Awake()
        {
            Health = MaxHealth;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;

            if (Health < 0)
            {
                Health = 0;
            }

            if (Health == 0)
            {
                Destroy(gameObject);

                Destroyed?.Invoke(this);
            }
        }
    }
}