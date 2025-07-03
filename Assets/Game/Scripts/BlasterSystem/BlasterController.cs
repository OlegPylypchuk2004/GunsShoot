using UnityEngine;
using VContainer;

namespace BlasterSystem
{
    public class BlasterController : MonoBehaviour
    {
        [SerializeField] private float _minRotationAngle;
        [SerializeField] private float _maxRotationAngle;
        [SerializeField] private Camera _camera;

        private BlasterHolder _blasterHolder;
        private float _offsetAngle;

        [Inject]
        private void Construct(BlasterHolder blasterHolder)
        {
            _blasterHolder = blasterHolder;
        }

        private void OnEnable()
        {
            if (_blasterHolder.Blaster != null)
            {
                _blasterHolder.Blaster.transform.SetParent(transform, false);
            }

            _blasterHolder.BlasterChanged += OnBlasterChanged;
        }

        private void OnDisable()
        {
            _blasterHolder.BlasterChanged -= OnBlasterChanged;
        }

        public void UpdateRotation()
        {
            if (Input.GetMouseButtonDown(1))
            {
                Vector2 initialDirection = GetInputDirection();
                _offsetAngle = Mathf.Atan2(initialDirection.y, initialDirection.x) * Mathf.Rad2Deg - transform.eulerAngles.z;
            }
            else if (Input.GetMouseButton(1))
            {
                Vector2 direction = GetInputDirection();
                float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - _offsetAngle;
                targetAngle = Mathf.Clamp(targetAngle, _minRotationAngle, _maxRotationAngle);

                transform.rotation = Quaternion.Euler(0, 0, targetAngle);
            }
        }

        private Vector2 GetInputDirection()
        {
            Vector3 mouseWorldPosition = GetMouseWorldPosition();
            Vector2 direction = mouseWorldPosition - transform.position;

            return direction.normalized;
        }

        private Vector3 GetMouseWorldPosition()
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Mathf.Abs(_camera.transform.position.z);

            return _camera.ScreenToWorldPoint(mousePosition);
        }

        private void OnBlasterChanged(Blaster blaster)
        {
            if (blaster == null)
            {
                return;
            }

            _blasterHolder.Blaster.transform.SetParent(transform, false);
        }
    }
}