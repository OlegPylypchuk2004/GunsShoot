using ObstacleSystem;
using System;

namespace HealthSystem
{
    public class HealthManager : IDisposable
    {
        private int _health;

        public int Health
        {
            get => _health;
            private set
            {
                if (value < 0 || value == _health)
                {
                    return;
                }

                _health = value;

                HealthChanged?.Invoke(Health);
            }
        }

        private ObstacleContainer _obstacleContainer;

        public event Action<int> HealthChanged;

        public HealthManager(ObstacleContainer obstacleContainer)
        {
            _obstacleContainer = obstacleContainer;

            _obstacleContainer.ObstacleAdded += OnObstacleAdded;
            _obstacleContainer.ObstacleRemoved += OnObstacleRemoved;

            foreach (Obstacle obstacle in _obstacleContainer.Obstacles)
            {
                obstacle.Fallen += OnObstacleFallen;
            }
        }

        public void Dispose()
        {
            _obstacleContainer.ObstacleAdded += OnObstacleAdded;
            _obstacleContainer.ObstacleRemoved += OnObstacleRemoved;

            foreach (Obstacle obstacle in _obstacleContainer.Obstacles)
            {
                obstacle.Fallen -= OnObstacleFallen;
            }
        }

        public void IncreaseHealth(int amount = 1)
        {
            Health += amount;
        }

        private void OnObstacleAdded(Obstacle obstacle)
        {
            obstacle.Fallen += OnObstacleFallen;
        }

        private void OnObstacleRemoved(Obstacle obstacle)
        {
            obstacle.Fallen -= OnObstacleFallen;
        }

        private void OnObstacleFallen(Obstacle obstacle)
        {
            Health -= 1;
        }
    }
}