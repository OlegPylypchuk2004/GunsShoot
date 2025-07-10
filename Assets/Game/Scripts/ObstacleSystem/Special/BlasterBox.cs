using UnityEngine;
using UnityEngine.UI;

namespace ObstacleSystem.Special
{
    public class BlasterBox : Obstacle
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Image _blasterPreviewImage;

        private void Awake()
        {
            _canvas.worldCamera = Camera.main;
        }

        private void LateUpdate()
        {
            _canvas.transform.rotation = Quaternion.identity;
        }
    }
}