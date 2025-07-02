using DG.Tweening;
using UnityEngine;

namespace ObstacleSystem
{
    public class ObstacleShaker : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float _duration;
        [SerializeField, Min(0f)] private float _strength;
        [SerializeField] private Ease _ease;
        [SerializeField] private Obstacle _obstacle;
        [SerializeField] private Transform _meshTransform;

        private Vector3 _normalMeshScale;
        private Tween _currentTween;

        private void Awake()
        {
            _normalMeshScale = _meshTransform.localScale;
        }

        private void OnEnable()
        {
            _obstacle.HealthChanged += OnObstacleHealthChanged;
        }

        private void OnDisable()
        {
            _obstacle.HealthChanged -= OnObstacleHealthChanged;
        }

        private void OnObstacleHealthChanged(int health)
        {
            if (health <= 0)
            {
                return;
            }

            _currentTween?.Kill();
            _meshTransform.localScale = _normalMeshScale;

            _currentTween = _meshTransform.DOPunchScale(_normalMeshScale * _strength, _duration, 1)
                .SetEase(_ease)
                .SetLink(gameObject);
        }
    }
}