using ObstacleSystem;
using UnityEngine;

namespace Gameplay.Test
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [SerializeField] private Obstacle[] _obstaclePrefabs;

        private Obstacle _currentObstacle;

        private void Start()
        {
            _currentObstacle = SpawnObstacle();
            _currentObstacle.Destroyed += OnObstacleDestroyed;
        }

        private void OnDestroy()
        {
            if (_currentObstacle != null)
            {
                _currentObstacle.Destroyed -= OnObstacleDestroyed;
            }
        }

        private Obstacle SpawnObstacle()
        {
            return Instantiate(_obstaclePrefabs[Random.Range(0, _obstaclePrefabs.Length)], transform);
        }

        private void OnObstacleDestroyed(Obstacle obstacle)
        {
            obstacle.Destroyed -= OnObstacleDestroyed;

            _currentObstacle = SpawnObstacle();
            _currentObstacle.Destroyed += OnObstacleDestroyed;
        }
    }
}