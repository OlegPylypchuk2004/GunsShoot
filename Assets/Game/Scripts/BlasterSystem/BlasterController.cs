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
            if (_inputHandler.IsAim)
            {
                Vector2 direction = GetInputDirection();
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                angle = Mathf.Repeat(angle + 180f, 360f) - 180f;
                angle = Mathf.Clamp(angle, _minRotationAngle, _maxRotationAngle);

                transform.rotation = Quaternion.Euler(0, 0, angle);
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
            Vector2 mousePosition = Input.mousePosition;
            mousePosition.y = Mathf.Clamp(mousePosition.y, 0, Screen.height);
            mousePosition.x = Mathf.Clamp(mousePosition.x, 0, Screen.width);

            Vector3 position = new Vector3(mousePosition.x, mousePosition.y, Mathf.Abs(_camera.transform.position.z));

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