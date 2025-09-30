using Effects;
using UnityEngine;

namespace ObstacleSystem
{
    public class ObstacleDestroyAnimator : MonoBehaviour
    {
        [SerializeField] private Obstacle _obstacle;
        [SerializeField] private ObstacleDestroyEffect _effectPrefab;
        [SerializeField] private Vector3 _effectPositionOffset;

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

            Instantiate(_effectPrefab, transform.position + _effectPositionOffset, Quaternion.Euler(Vector3.zero));
        }
    }
}