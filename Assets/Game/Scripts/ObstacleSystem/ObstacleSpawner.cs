using System.Collections;
using System.Linq;
using UnityEngine;
using VContainer;

namespace ObstacleSystem
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [SerializeField] private Obstacle[] _obstaclePrefabs;
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField, Min(0f)] private float _minLaunchForce;
        [SerializeField, Min(0f)] private float _maxLaunchForce;

        private ObstacleContainer _obstacleContainer;
        private Coroutine _spawnObstaclesCoroutine;

        [Inject]
        private void Construct(ObstacleContainer obstacleContainer)
        {
            _obstacleContainer = obstacleContainer;
        }

        private void OnEnable()
        {
            foreach (Obstacle obstacle in _obstacleContainer.Obstacles)
            {
                obstacle.Destroyed += OnObstacleDestroyed;
                obstacle.Fallen += OnObstacleFallen;
            }
        }

        private void OnDisable()
        {
            foreach (Obstacle obstacle in _obstacleContainer.Obstacles)
            {
                obstacle.Destroyed -= OnObstacleDestroyed;
                obstacle.Fallen -= OnObstacleFallen;
            }
        }

        public void Activate()
        {
            if (_spawnObstaclesCoroutine != null)
            {
                StopCoroutine(_spawnObstaclesCoroutine);
                _spawnObstaclesCoroutine = null;
            }

            _spawnObstaclesCoroutine = StartCoroutine(SpawnObstaclesCoroutine());
        }

        public void Deactivate()
        {
            if (_spawnObstaclesCoroutine != null)
            {
                StopCoroutine(_spawnObstaclesCoroutine);
                _spawnObstaclesCoroutine = null;
            }
        }

        private IEnumerator SpawnObstaclesCoroutine()
        {
            while (true)
            {
                int obstaclesToSpawnAmount = Random.Range(1, _spawnPoints.Length + 1);
                Transform[] spawnPoints = _spawnPoints.OrderBy(x => Random.value).ToArray();

                for (int i = 0; i < obstaclesToSpawnAmount; i++)
                {
                    Obstacle obstacle = SpawnObstacle();
                    obstacle.transform.position = spawnPoints[i].position;
                    obstacle.Launch(spawnPoints[i].up * Random.Range(_minLaunchForce, _maxLaunchForce));

                    obstacle.Destroyed += OnObstacleDestroyed;
                    obstacle.Fallen += OnObstacleFallen;
                }

                yield return new WaitUntil(() => _obstacleContainer.Obstacles.Count == 0);
            }
        }

        private Obstacle SpawnObstacle()
        {
            Obstacle obstacle = Instantiate(_obstaclePrefabs[Random.Range(0, _obstaclePrefabs.Length)]);
            _obstacleContainer.TryAddObstacle(obstacle);

            return obstacle;
        }

        private void OnObstacleDestroyed(Obstacle obstacle)
        {
            _obstacleContainer.TryRemoveObstacle(obstacle);

            obstacle.Destroyed -= OnObstacleDestroyed;
            obstacle.Fallen -= OnObstacleFallen;
        }

        private void OnObstacleFallen(Obstacle obstacle)
        {
            _obstacleContainer.TryRemoveObstacle(obstacle);

            obstacle.Destroyed -= OnObstacleDestroyed;
            obstacle.Fallen -= OnObstacleFallen;
        }
    }
}