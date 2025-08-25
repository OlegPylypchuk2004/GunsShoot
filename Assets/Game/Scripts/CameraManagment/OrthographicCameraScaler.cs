using UnityEngine;

namespace CameraManagment
{
    public class OrthographicCameraScaler : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Vector2Int _exampleScreenSize;

        private float _exampleCameraSize;

        private void Awake()
        {
            _exampleCameraSize = _camera.orthographicSize;
        }

        private void Start()
        {
            Scale();
        }

        public void Scale()
        {
            float exampleScreenCorrelation = (float)_exampleScreenSize.x / (float)_exampleScreenSize.y;
            float currentScreenCorrelation = (float)Screen.width / (float)Screen.height;
            float currentCameraSize = currentScreenCorrelation * _exampleCameraSize / exampleScreenCorrelation;

            _camera.orthographicSize = currentCameraSize;
        }
    }
}