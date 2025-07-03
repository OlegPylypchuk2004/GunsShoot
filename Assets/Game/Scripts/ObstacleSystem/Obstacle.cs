using DamageSystem;
using System;
using UnityEngine;

namespace ObstacleSystem
{
    public class Obstacle : MonoBehaviour, IDamageable
    {
        [field: SerializeField] public int MaxHealth { get; private set; }

        [SerializeField] private float _minAngularVelocity;
        [SerializeField] private float _maxAngularVelocity;
        [SerializeField] private Rigidbody _rigidbody;

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
        public event Action<Obstacle, int> Damaged;
        public event Action<Obstacle> Destroyed;

        private void Awake()
        {
            Health = MaxHealth;
        }

        private void Start()
        {
            ApplyRotation();
            ApplyAngularVelocity();
        }

        public void Launch(Vector3 direction)
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.AddForce(direction, ForceMode.Impulse);
        }

        public void TakeDamage(int damage)
        {
            if (damage > Health)
            {
                damage = Health;
            }

            Health -= damage;

            Damaged?.Invoke(this, damage);

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

        private void ApplyRotation()
        {
            transform.rotation = UnityEngine.Random.rotation;
        }

        private void ApplyAngularVelocity()
        {
            float randomX = UnityEngine.Random.Range(_minAngularVelocity, _maxAngularVelocity);
            float randomY = UnityEngine.Random.Range(_minAngularVelocity, _maxAngularVelocity);
            float randomZ = UnityEngine.Random.Range(_minAngularVelocity, _maxAngularVelocity);

            _rigidbody.angularVelocity = new Vector3(randomX, randomY, randomZ);
        }
    }
}