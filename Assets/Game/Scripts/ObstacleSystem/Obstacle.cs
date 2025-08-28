using DamageSystem;
using System;
using UnityEngine;

namespace ObstacleSystem
{
    public class Obstacle : MonoBehaviour, IDamageable
    {
        [field: SerializeField] public int MaxHealth { get; private set; }

        [SerializeField] private float _minYPosition;
        [SerializeField] private float _minAngularVelocity;
        [SerializeField] private float _maxAngularVelocity;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Transform _meshTransform;

        private float _gravityMultiplier;
        private int _health;
        private Vector3 _meshAngularVelocity;

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
        public event Action<Obstacle> Fallen;

        private void Awake()
        {
            Health = MaxHealth;
        }

        private void Start()
        {
            ApplyRotation();
        }

        private void Update()
        {
            if (transform.position.y <= _minYPosition)
            {
                Destroy(gameObject);

                Fallen?.Invoke(this);
            }

            _meshTransform.Rotate(_meshAngularVelocity * Time.deltaTime);
        }

        private void FixedUpdate()
        {
            Vector3 gravity = Physics.gravity * _gravityMultiplier;
            _rigidbody.AddForce(gravity, ForceMode.Acceleration);
        }

        public void Launch(Vector3 direction, float gravityMultiplier)
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.AddForce(direction, ForceMode.Impulse);
            _gravityMultiplier = gravityMultiplier;
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
                Destroyed?.Invoke(this);

                Kill();
            }
        }

        protected virtual void Kill()
        {
            Destroy(gameObject);
        }

        private void ApplyRotation()
        {
            _meshTransform.rotation = UnityEngine.Random.rotation;

            float randomX = UnityEngine.Random.Range(_minAngularVelocity, _maxAngularVelocity);
            float randomY = UnityEngine.Random.Range(_minAngularVelocity, _maxAngularVelocity);
            float randomZ = UnityEngine.Random.Range(_minAngularVelocity, _maxAngularVelocity);

            _meshAngularVelocity = new Vector3(randomX, randomY, randomZ);
        }
    }
}