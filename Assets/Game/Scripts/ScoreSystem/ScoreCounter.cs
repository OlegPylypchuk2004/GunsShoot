using ComboSystem;
using ObstacleSystem;
using System;
using UnityEngine;

namespace ScoreSystem
{
    public class ScoreCounter
    {
        private ObstacleContainer _obstacleContainer;
        private ComboCounter _comboCounter;
        private int _score;

        public event Action<int> ScoreChanged;

        public ScoreCounter(ObstacleContainer obstacleContainer, ComboCounter comboCounter)
        {
            _obstacleContainer = obstacleContainer;
            _comboCounter = comboCounter;

            _obstacleContainer.ObstacleAdded += OnObstacleAdded;
            _obstacleContainer.ObstacleRemoved += OnObstacleRemoved;
        }

        ~ScoreCounter()
        {
            _obstacleContainer.ObstacleAdded -= OnObstacleAdded;
            _obstacleContainer.ObstacleRemoved -= OnObstacleRemoved;
        }

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
            Score += Mathf.FloorToInt(obstacle.MaxHealth * _comboCounter.ScoreMultiplier);
        }
    }
}