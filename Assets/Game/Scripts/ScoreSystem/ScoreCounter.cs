using ObstacleSystem;
using System;

namespace ScoreSystem
{
    public class ScoreCounter
    {
        private int _score;
        private ObstacleContainer _obstacleContainer;

        public event Action<int> ScoreChanged;

        public int Score
        {
            get
            {
                return _score;
            }
            private set
            {
                if (_score == value)
                {
                    return;
                }

                _score = value;

                ScoreChanged?.Invoke(_score);
            }
        }

        public ScoreCounter(ObstacleContainer obstacleContainer)
        {
            _obstacleContainer = obstacleContainer;
            _obstacleContainer.ObstacleAdded += OnObstacleAdded;
            _obstacleContainer.ObstacleRemoved += OnObstacleRemoved;
        }

        ~ScoreCounter()
        {
            _obstacleContainer.ObstacleAdded -= OnObstacleAdded;
            _obstacleContainer.ObstacleRemoved -= OnObstacleRemoved;
        }

        private void OnObstacleAdded(Obstacle obstacle)
        {
            obstacle.Destroyed += OnObstacleDestroyed;
        }

        private void OnObstacleRemoved(Obstacle obstacle)
        {
            obstacle.Destroyed -= OnObstacleDestroyed;
        }

        private void OnObstacleDestroyed(Obstacle obstacle)
        {
            Score += obstacle.MaxHealth;
        }
    }
}