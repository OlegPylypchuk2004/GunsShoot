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

        private bool _wasAim;
        private float _angleOffset;

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

            if (isAim && !_wasAim)
            {
                Vector2 direction = GetInputDirection();
                float rawAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                rawAngle = Mathf.Repeat(rawAngle + 180f, 360f) - 180f;

                float currentAngle = transform.rotation.eulerAngles.z;
                currentAngle = Mathf.Repeat(currentAngle + 180f, 360f) - 180f;

                _angleOffset = currentAngle - rawAngle;
            }

            _wasAim = isAim;

            if (isAim)
            {
                Vector2 aimDirection = GetInputDirection();
                float aimRawAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
                aimRawAngle = Mathf.Repeat(aimRawAngle + 180f, 360f) - 180f;

                float finalAngle = aimRawAngle + _angleOffset;
                finalAngle = Mathf.Clamp(finalAngle, _minRotationAngle, _maxRotationAngle);

                transform.rotation = Quaternion.Euler(0, 0, finalAngle);
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
            Vector2 pointerPosition = _inputHandler.AimPointerPosition;
            pointerPosition.y = Mathf.Clamp(pointerPosition.y, 0, Screen.height);
            pointerPosition.x = Mathf.Clamp(pointerPosition.x, 0, Screen.width);

            Vector3 position = new Vector3(pointerPosition.x, pointerPosition.y, Mathf.Abs(_camera.transform.position.z));

            return _camera.ScreenToWorldPoint(position);
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