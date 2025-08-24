using InputSystem;
using UnityEngine;
using VContainer;

namespace BlasterSystem
{
    public class BlasterController : MonoBehaviour
    {
        [SerializeField] private float _minRotationAngle;
        [SerializeField] private float _maxRotationAngle;
        [SerializeField] private Camera _camera;

        private IInputHandler _inputHandler;
        private BlasterHolder _blasterHolder;
        private float _offsetAngle;
        private bool _isAimCashed;

        [Inject]
        private void Construct(IInputHandler inputHandler, BlasterHolder blasterHolder)
        {
            _inputHandler = inputHandler;
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
            bool isAim = _inputHandler.IsAim;

            if (isAim)
            {
                if (!_isAimCashed)
                {
                    Vector2 initialDirection = GetInputDirection();
                    float angleToMouse = Mathf.Atan2(initialDirection.y, initialDirection.x) * Mathf.Rad2Deg;
                    float currentZ = transform.eulerAngles.z;
                    float delta = Mathf.DeltaAngle(currentZ, angleToMouse);
                    _offsetAngle = delta;
                }
                else
                {
                    Vector2 direction = GetInputDirection();
                    float angleToMouse = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    float targetAngle = angleToMouse - _offsetAngle;

                    targetAngle = Mathf.Repeat(targetAngle + 180f, 360f) - 180f;
                    targetAngle = Mathf.Clamp(targetAngle, _minRotationAngle, _maxRotationAngle);

                    transform.rotation = Quaternion.Euler(0, 0, targetAngle);
                }
            }

            _isAimCashed = isAim;
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