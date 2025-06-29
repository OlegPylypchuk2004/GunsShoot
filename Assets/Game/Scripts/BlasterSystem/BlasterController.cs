using UnityEngine;

namespace BlasterSystem
{
    public class BlasterController : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        private float _offsetAngle;

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                Vector2 initialDirection = GetInputDirection();
                _offsetAngle = Mathf.Atan2(initialDirection.y, initialDirection.x) * Mathf.Rad2Deg - transform.eulerAngles.z;
            }

            if (Input.GetMouseButton(1))
            {
                Rotate();
            }
        }

        private void Rotate()
        {
            Vector2 direction = GetInputDirection();
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, targetAngle - _offsetAngle);
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