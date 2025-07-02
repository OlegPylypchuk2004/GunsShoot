using ObstacleSystem;
using System.Collections;
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

            StartCoroutine(SpawnObstacleWithDelay());
        }

        private IEnumerator SpawnObstacleWithDelay()
        {
            yield return new WaitForSeconds(1f);

            _currentObstacle = SpawnObstacle();
            _currentObstacle.Destroyed += OnObstacleDestroyed;
        }
    }
}