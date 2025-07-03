using DG.Tweening;
using UnityEngine;

namespace ObstacleSystem
{
    public class ObstacleDestroyAnimator : MonoBehaviour
    {
        [SerializeField] private Obstacle _obstacle;
        [SerializeField, Min(0f)] private float _duration;
        [SerializeField, Min(0f)] private float _delay;
        [SerializeField] private Ease _ease;

        private void OnEnable()
        {
            _obstacle.Destroyed += OnObstacleDestroyed;
        }

        private void OnDisable()
        {
            _obstacle.Destroyed -= OnObstacleDestroyed;
        }

        private void OnObstacleDestroyed(Obstacle obstacle)
        {
            _obstacle.Destroyed -= OnObstacleDestroyed;

            transform.SetParent(null);

            transform.DOScale(0f, _duration)
                .SetEase(_ease)
                .SetDelay(_delay)
                .SetLink(gameObject)
                .OnKill(() =>
                {
                    Destroy(gameObject);
                });
        }
    }
}