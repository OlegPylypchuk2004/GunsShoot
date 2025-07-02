using DamageSystem;
using UnityEngine;
using VContainer;

namespace ObstacleSystem
{
    public class ObstacleDamageNumberSpawner : MonoBehaviour
    {
        [SerializeField] private DamageNumber _damageNumberPrefab;
        [SerializeField] private Vector3 _spawnPositionOffset;
        [SerializeField, Min(0f)] private float _spawnPositionRadius;

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
            damageNumber.transform.position = obstacle.transform.position + GetSpawnDamageNumberPosition();
            damageNumber.PlayAnimation(damage, Color.white);
        }

        private Vector3 GetSpawnDamageNumberPosition()
        {
            return _spawnPositionOffset + new Vector3(Random.Range(-_spawnPositionRadius, _spawnPositionRadius), Random.Range(-_spawnPositionRadius, _spawnPositionRadius), 0f);
        }
    }
}