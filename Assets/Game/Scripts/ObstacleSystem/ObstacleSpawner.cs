using System.Collections;
using System.Collections.Generic;
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
        private List<Obstacle> _activeObstacles;

        [Inject]
        private void Construct(ObstacleContainer obstacleContainer)
        {
            _obstacleContainer = obstacleContainer;
        }

        private void Awake()
        {
            _activeObstacles = new List<Obstacle>();
        }

        private void Start()
        {
            _spawnObstaclesCoroutine = StartCoroutine(SpawnObstaclesCoroutine());
        }

        private void OnDestroy()
        {
            foreach (Obstacle obstacle in _obstacleContainer.Obstacles)
            {
                obstacle.Destroyed -= OnObstacleDestroyed;
            }
        }

        private IEnumerator SpawnObstaclesCoroutine()
        {
            yield return new WaitForSeconds(1f);

            while (true)
            {
                int obstaclesToSpawnAmount = Random.Range(1, _spawnPoints.Length + 1);
                Transform[] spawnPoints = _spawnPoints.OrderBy(x => Random.value).ToArray();

                for (int i = 0; i < obstaclesToSpawnAmount; i++)
                {
                    Obstacle obstacle = SpawnObstacle();
                    obstacle.transform.position = spawnPoints[i].position;
                    obstacle.Launch(spawnPoints[i].up * Random.Range(_minLaunchForce, _maxLaunchForce));

                    _activeObstacles.Add(obstacle);
                    obstacle.Destroyed += OnObstacleDestroyed;
                }

                yield return new WaitUntil(() => _activeObstacles.Count == 0);
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
            obstacle.Destroyed -= OnObstacleDestroyed;
            _activeObstacles.Remove(obstacle);
        }
    }
}