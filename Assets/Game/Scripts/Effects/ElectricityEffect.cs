using UnityEngine;

namespace Effects
{
    public class ElectricityEffect : MonoBehaviour
    {
        [SerializeField] private Transform _firstPoint;
        [SerializeField] private Transform _secondPoint;

        private Vector3 _initialPoint;
        private Vector3 _targetPoint;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Play(_firstPoint.position, _secondPoint.position);
            }
        }

        public void Play(Vector3 initialPoint, Vector3 targetPoint)
        {
            _initialPoint = initialPoint;
            _targetPoint = targetPoint;

            UpdatePosition();
            UpdateRotation();
            UpdateScale();
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