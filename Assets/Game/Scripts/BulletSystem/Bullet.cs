using UnityEngine;

namespace BulletSystem
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;

        private BulletState _state;
        private float _speed;
        private Vector3 _direction;

        private void Awake()
        {
            _state = BulletState.Idle;
        }

        private void FixedUpdate()
        {
            if (_state == BulletState.Launched)
            {
                _rigidbody.velocity = _direction * _speed;
            }
        }

        public void Launch(float speed, Vector3 direction)
        {
            _speed = speed;
            _direction = direction.normalized;
            _state = BulletState.Launched;
        }
    }
}