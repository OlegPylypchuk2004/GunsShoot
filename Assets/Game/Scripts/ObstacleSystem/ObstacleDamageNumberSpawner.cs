using DamageSystem;
using UnityEngine;
using VContainer;

namespace ObstacleSystem
{
    public class ObstacleDamageNumberSpawner : MonoBehaviour
    {
        [SerializeField] private DamageNumber _damageNumberPrefab;

        private ObstacleContainer _obstacleContainer;

        [Inject]
        private void Construct(ObstacleContainer obstacleContainer)
        {
            _obstacleContainer = obstacleContainer;
        }

        private void OnEnable()
        {
            _obstacleContainer.ObstacleAdded += OnObstacleAdded;
            _obstacleContainer.ObstacleRemoved += OnObstacleRemoved;

            foreach (Obstacle obstacle in _obstacleContainer.Obstacles)
            {
                obstacle.Damaged -= OnObstacleDamaged;
            }
        }

        private void OnDisable()
        {
            _obstacleContainer.ObstacleAdded -= OnObstacleAdded;
            _obstacleContainer.ObstacleRemoved -= OnObstacleRemoved;

            foreach (Obstacle obstacle in _obstacleContainer.Obstacles)
            {
                obstacle.Damaged -= OnObstacleDamaged;
            }
        }

        private void OnObstacleAdded(Obstacle obstacle)
        {
            obstacle.Damaged += OnObstacleDamaged;
        }

        private void OnObstacleRemoved(Obstacle obstacle)
        {
            obstacle.Damaged -= OnObstacleDamaged;
        }

        private void OnObstacleDamaged(Obstacle obstacle, int damage)
        {
            DamageNumber damageNumber = Instantiate(_damageNumberPrefab);
            damageNumber.transform.position = obstacle.transform.position + Vector3.up * 0.275f;
            damageNumber.PlayAnimation(damage, Color.white);
        }
    }
}