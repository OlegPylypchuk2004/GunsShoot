using UnityEngine;
using VContainer;

namespace BlasterSystem
{
    public class BlasterController : MonoBehaviour
    {
        private Camera _camera;
        private float _offsetAngle;

        [Inject]
        private void Construct(Camera camera)
        {
            _camera = camera;
        }

        public void UpdateRotation()
        {
            if (Input.GetMouseButtonDown(1))
            {
                Vector2 initialDirection = GetInputDirection();
                _offsetAngle = Mathf.Atan2(initialDirection.y, initialDirection.x) * Mathf.Rad2Deg - transform.eulerAngles.z;
            }

            if (Input.GetMouseButton(1))
            {
                Vector2 direction = GetInputDirection();
                float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                transform.rotation = Quaternion.Euler(0, 0, targetAngle - _offsetAngle);
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
    }
}