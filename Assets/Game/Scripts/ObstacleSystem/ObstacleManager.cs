using System.Collections;
using UnityEngine;
using VContainer;

namespace ObstacleSystem
{
    public class ObstacleManager : MonoBehaviour
    {
        [SerializeField] private Obstacle[] _obstaclePrefabs;
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField, Min(0f)] private float _minLaunchForce;
        [SerializeField, Min(0f)] private float _maxLaunchForce;
        [SerializeField, Min(0f)] private float _spawnDelay;

        private ObstacleContainer _obstacleContainer;

        [Inject]
        private void Construct(ObstacleContainer obstacleContainer)
        {
            _obstacleContainer = obstacleContainer;
        }

        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(_spawnDelay);

                Transform spawnPoint = GetRandomSpawnPoint();
                Obstacle obstacle = SpawnObstacle();
                obstacle.transform.position = spawnPoint.position;
                obstacle.transform.rotation = spawnPoint.rotation;
                obstacle.Launch(Random.Range(_minLaunchForce, _maxLaunchForce));
                obstacle.Destroyed += OnObstacleDestroyed;
            }
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
            Obstacle obstacle = Instantiate(_obstaclePrefabs[Random.Range(0, _obstaclePrefabs.Length)]);
            _obstacleContainer.TryAddObstacle(obstacle);

            return obstacle;
        }

        private Transform GetRandomSpawnPoint()
        {
            return _spawnPoints[Random.Range(0, _spawnPoints.Length)];
        }

        private void OnObstacleDestroyed(Obstacle obstacle)
        {
            obstacle.Destroyed -= OnObstacleDestroyed;

            //StartCoroutine(SpawnObstacleWithDelay());
        }

        //private IEnumerator SpawnObstacleWithDelay()
        //{
        //    yield return new WaitForSeconds(1f);

        //    Transform spawnPoint = GetRandomSpawnPoint();
        //    Obstacle obstacle = SpawnObstacle();
        //    obstacle.transform.position = spawnPoint.position;
        //    obstacle.transform.rotation = spawnPoint.rotation;
        //    obstacle.Launch(Random.Range(_minLaunchForce, _maxLaunchForce));
        //    obstacle.Destroyed += OnObstacleDestroyed;
        //}
    }
}