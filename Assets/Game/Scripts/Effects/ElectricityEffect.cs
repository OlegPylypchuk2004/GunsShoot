using UnityEngine;

namespace Effects
{
    public class ElectricityEffect : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private Vector3 _initialPoint;
        private Vector3 _targetPoint;

        public void Play(Vector3 initialPoint, Vector3 targetPoint)
        {
            _initialPoint = initialPoint;
            _targetPoint = targetPoint;

            UpdatePosition();
            UpdateRotation();
            UpdateScale();

            _animator.SetTrigger("Play");
        }

        private void UpdatePosition()
        {
            transform.position = _initialPoint;
        }

        private void UpdateRotation()
        {
            Vector3 direction = _targetPoint - _initialPoint;
            float angle = Vector2.SignedAngle(Vector2.right, new Vector2(direction.x, direction.y));

            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        private void UpdateScale()
        {
            float distanceBetweenPoints = Vector3.Distance(_initialPoint, _targetPoint);

            transform.localScale = Vector3.one * distanceBetweenPoints;
        }
    }
}