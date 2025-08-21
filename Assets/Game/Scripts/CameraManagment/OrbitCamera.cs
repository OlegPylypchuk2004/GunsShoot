using UnityEngine;
using UnityEngine.EventSystems;

namespace CameraManagment
{
    public class OrbitCamera : MonoBehaviour
    {
        [SerializeField] private Transform _targetTransform;
        [SerializeField] private float _sensitivity;
        [SerializeField] private EventSystem _eventSystem;

        private Vector3 _defaultRotation;
        private float _mouseX;

        public bool IsInteractible { get; private set; }

        private void Awake()
        {
            _defaultRotation = transform.rotation.eulerAngles;
        }

        private void OnEnable()
        {
            transform.rotation = Quaternion.Euler(_defaultRotation);
        }

        private void Update()
        {
            if (!IsInteractible)
            {
                return;
            }

            if (_eventSystem.IsPointerOverGameObject())
            {
                return;
            }

            if (Input.GetMouseButton(0))
            {
                _mouseX = Input.GetAxis("Mouse X");
                transform.eulerAngles += new Vector3(transform.eulerAngles.x, _mouseX * _sensitivity, transform.eulerAngles.z);
            }
        }

        public void SetInteractible(bool isInteractible)
        {
            IsInteractible = isInteractible;
        }
    }
}