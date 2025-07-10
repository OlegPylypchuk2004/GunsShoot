using StageSystem;
using System.Collections;
using System.Linq;
using UnityEngine;
using VContainer;

namespace ObstacleSystem
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [SerializeField] private Obstacle _obstaclePrefab;
        [SerializeField] private Obstacle[] _specialObstaclePrefabs;
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField, Min(0f)] private float _minLaunchForce;
        [SerializeField, Min(0f)] private float _maxLaunchForce;

        private ObstacleContainer _obstacleContainer;
        private StageManager _stageManager;
        private DestroyObstacleResolver _destroyObstacleResolver;
        private Coroutine _spawnObstaclesCoroutine;
        private int _obstaclesBeforeSpecial;

        [Inject]
        private void Construct(ObstacleContainer obstacleContainer, StageManager stageManager, DestroyObstacleResolver destroyObstacleResolver)
        {
            _obstacleContainer = obstacleContainer;
            _stageManager = stageManager;
            _destroyObstacleResolver = destroyObstacleResolver;
        }

        private void Awake()
        {
            _obstaclesBeforeSpecial = Random.Range(5, 10);
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
                    obstacle.Launch(spawnPoints[i].up * Random.Range(_minLaunchForce, _maxLaunchForce) * _stageManager.StageData.ObstacleLaunchForceMultiplier, _stageManager.StageData.ObstacleGravityMultiplier);

                    obstacle.Destroyed += OnObstacleDestroyed;
                    obstacle.Fallen += OnObstacleFallen;
                }

                yield return new WaitUntil(() => _obstacleContainer.Obstacles.Count == 0);

                _stageManager.IncreaseStageNumber();
            }
        }

        private Obstacle SpawnObstacle()
        {
            Obstacle obstaclePrefab = _obstaclePrefab;

            if (_obstaclesBeforeSpecial <= 0)
            {
                obstaclePrefab = _specialObstaclePrefabs[Random.Range(0, _specialObstaclePrefabs.Length)];
                _obstaclesBeforeSpecial = Random.Range(5, 10);
            }
            else
            {
                _obstaclesBeforeSpecial -= 1;
            }

            Obstacle obstacle = Instantiate(obstaclePrefab);
            _obstacleContainer.TryAddObstacle(obstacle);

            return obstacle;
        }

        private void OnObstacleDestroyed(Obstacle obstacle)
        {
            _obstacleContainer.TryRemoveObstacle(obstacle);
            _destroyObstacleResolver.Resolve(obstacle);

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