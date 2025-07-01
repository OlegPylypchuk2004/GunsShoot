using UnityEngine;

namespace CameraManagment
{
    public class OrbitCamera : MonoBehaviour
    {
        [SerializeField] private Transform _targetTransform;
        [SerializeField] private float _sensitivity;

        private float _mouseX;

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                _mouseX = Input.GetAxis("Mouse X");
                transform.eulerAngles += new Vector3(transform.eulerAngles.x, _mouseX * _sensitivity, transform.eulerAngles.z);
            }
        }
    }
}