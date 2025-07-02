using System.Collections;
using UnityEngine;
using VContainer;

namespace ObstacleSystem
{
    public class ObstacleManager : MonoBehaviour
    {
        [SerializeField] private Obstacle[] _obstaclePrefabs;

        private ObstacleContainer _obstacleContainer;

        [Inject]
        private void Construct(ObstacleContainer obstacleContainer)
        {
            _obstacleContainer = obstacleContainer;
        }

        private void Start()
        {
            Obstacle obstacle = SpawnObstacle();
            obstacle.Destroyed += OnObstacleDestroyed;
        }

        private void OnDestroy()
        {
            foreach (Obstacle obstacle in _obstacleContainer.Obstacles)
            {
                obstacle.Destroyed -= OnObstacleDestroyed;
            }
        }

        private Obstacle SpawnObstacle()
        {
            Obstacle obstacle = Instantiate(_obstaclePrefabs[Random.Range(0, _obstaclePrefabs.Length)], transform);
            _obstacleContainer.TryAddObstacle(obstacle);

            return obstacle;
        }

        private void OnObstacleDestroyed(Obstacle obstacle)
        {
            obstacle.Destroyed -= OnObstacleDestroyed;

            StartCoroutine(SpawnObstacleWithDelay());
        }

        private IEnumerator SpawnObstacleWithDelay()
        {
            yield return new WaitForSeconds(1f);

            Obstacle obstacle = SpawnObstacle();
            obstacle.Destroyed += OnObstacleDestroyed;
        }
    }
}