using BlasterSystem;
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
        [SerializeField] private Color _defaultNumberColor;
        [SerializeField] private Color _criticalDamageNumberColor;

        private ObstacleContainer _obstacleContainer;
        private BlasterHolder _blasterHolder;

        [Inject]
        private void Construct(ObstacleContainer obstacleContainer, BlasterHolder blasterHolder)
        {
            _obstacleContainer = obstacleContainer;
            _blasterHolder = blasterHolder;
        }

        private void OnEnable()
        {
            _obstacleContainer.ObstacleAdded += OnObstacleAdded;
            _obstacleContainer.ObstacleRemoved += OnObstacleRemoved;

            foreach (Obstacle obstacle in _obstacleContainer.Obstacles)
            {
                obstacle.Damaged += OnObstacleDamaged;
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
            damageNumber.PlayAnimation(damage, GetNumberColor(damage));
        }

        private Vector3 GetSpawnDamageNumberPosition()
        {
            return _spawnPositionOffset + new Vector3(Random.Range(-_spawnPositionRadius, _spawnPositionRadius), Random.Range(-_spawnPositionRadius, _spawnPositionRadius), 0f);
        }

        private Color GetNumberColor(int damage)
        {
            if (IsCriticalDamage(damage))
            {
                return _criticalDamageNumberColor;
            }

            return _defaultNumberColor;
        }

        private bool IsCriticalDamage(int damage)
        {
            if (damage > _blasterHolder.Blaster.Config.Damage)
            {
                return true;
            }

            return false;
        }
    }
}