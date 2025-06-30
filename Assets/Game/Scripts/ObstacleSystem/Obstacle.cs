using DamageSystem;
using System;
using UnityEngine;

namespace ObstacleSystem
{
    public class Obstacle : MonoBehaviour, IDamageable
    {
        [field: SerializeField] public int Health { get; private set; }

        public event Action<int> HealthChanged;

        public void TakeDamage(int damage)
        {
            Health -= damage;

            if (Health < 0)
            {
                Health = 0;
                Health = 100;
            }

            HealthChanged?.Invoke(Health);
        }
    }
}