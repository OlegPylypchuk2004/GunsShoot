using ObstacleSystem.Special;
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
        [SerializeField] private Bonus[] _bonusPrefabs;
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField, Min(0f)] private float _minLaunchForce;
        [SerializeField, Min(0f)] private float _maxLaunchForce;

        private ObstacleContainer _obstacleContainer;
        private StageManager _stageManager;
        private DestroyObstacleResolver _destroyObstacleResolver;
        private Coroutine _spawnObstaclesCoroutine;

        [Inject]
        private void Construct(ObstacleContainer obstacleContainer, StageManager stageManager, DestroyObstacleResolver destroyObstacleResolver)
        {
            _obstacleContainer = obstacleContainer;
            _stageManager = stageManager;
            _destroyObstacleResolver = destroyObstacleResolver;
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
                Debug.Log($"Stage: {_stageManager.StageNumber}");

                StageData stageData = _stageManager.StageData;
                int obstaclesToSpawnCount = Random.Range(stageData.MinObstaclesCount, stageData.MaxObstaclesCount);

                if (obstaclesToSpawnCount > _spawnPoints.Length)
                {
                    obstaclesToSpawnCount = _spawnPoints.Length;
                }

                Transform[] spawnPoints = _spawnPoints.OrderBy(spawnPoint => Random.value).ToArray();

                for (int i = 0; i < obstaclesToSpawnCount; i++)
                {
                    Obstacle obstaclePrefab = _obstaclePrefab;

                    if (i == 0)
                    {
                        if (Random.Range(0, 100) < stageData.SpawnBonusChance)
                        {
                            obstaclePrefab = _bonusPrefabs[Random.Range(0, _bonusPrefabs.Length)];
                        }
                    }

                    Obstacle obstacle = SpawnObstacle(obstaclePrefab);
                    obstacle.transform.position = spawnPoints[i].position;

                    Vector3 launchDirection = spawnPoints[i].up * Random.Range(_minLaunchForce, _maxLaunchForce) * stageData.ObstacleLaunchForceMultiplier;
                    float gravityMultiplier = stageData.ObstacleGravityMultiplier;
                    obstacle.Launch(launchDirection, gravityMultiplier);

                    obstacle.Destroyed += OnObstacleDestroyed;
                    obstacle.Fallen += OnObstacleFallen;
                }

                yield return new WaitUntil(() => _obstacleContainer.Obstacles.Count == 0);

                _stageManager.IncreaseStageNumber();
            }
        }

        private Obstacle SpawnObstacle(Obstacle obstaclePrefab)
        {
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