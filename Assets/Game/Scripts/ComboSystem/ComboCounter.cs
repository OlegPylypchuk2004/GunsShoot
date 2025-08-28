using ObstacleSystem;
using System;

namespace ComboSystem
{
    public class ComboCounter
    {
        private ObstacleContainer _obstacleContainer;

        private ComboStageData[] _stages;
        private int _stageIndex;
        private int _combo;
        private float _time;

        public event Action<int> ComboChanged;
        public event Action<float> TimeChanged;

        public ComboCounter(ObstacleContainer obstacleContainer, ComboConfig comboConfig)
        {
            _obstacleContainer = obstacleContainer;
            _stages = comboConfig.Stages;

            _obstacleContainer.ObstacleAdded += OnObstacleAdded;
            _obstacleContainer.ObstacleRemoved += OnObstacleRemoved;

            _stageIndex = -1;
        }

        ~ComboCounter()
        {
            _obstacleContainer.ObstacleAdded -= OnObstacleAdded;
            _obstacleContainer.ObstacleRemoved -= OnObstacleRemoved;
        }

        public int Combo
        {
            get
            {
                return _combo;
            }
            set
            {
                if (value == _combo)
                {
                    return;
                }

                _combo = value;

                ComboChanged?.Invoke(_combo);
            }
        }

        public float Time
        {
            get
            {
                return _time;
            }
            set
            {
                if (value == _time)
                {
                    return;
                }

                _time = value;

                TimeChanged?.Invoke(_time);
            }
        }

        public float ScoreMultiplier
        {
            get
            {
                return _stages[_stageIndex].ScoreMultiplier;
            }
        }

        public float NormalizedTime
        {
            get
            {
                return _time / _stages[_stageIndex].Time;
            }
        }

        public void Update()
        {
            if (_time > 0f)
            {
                Time -= UnityEngine.Time.deltaTime;

                if (_time <= 0f)
                {
                    ResetCombo();
                }
            }
        }

        private void OnObstacleAdded(Obstacle obstacle)
        {
            obstacle.Destroyed += OnObstacleDestroyed;
            obstacle.Fallen += OnObstacleFallen;
        }

        private void OnObstacleRemoved(Obstacle obstacle)
        {
            obstacle.Destroyed -= OnObstacleDestroyed;
            obstacle.Fallen -= OnObstacleFallen;
        }

        private void OnObstacleDestroyed(Obstacle obstacle)
        {
            IncreaseCombo();
        }

        private void OnObstacleFallen(Obstacle obstacle)
        {
            ResetCombo();
        }

        private void IncreaseCombo()
        {
            _stageIndex++;

            if (_stageIndex >= _stages.Length)
            {
                _stageIndex = _stages.Length - 1;
            }

            Combo++;
            Time = _stages[_stageIndex].Time;
        }

        private void ResetCombo()
        {
            _stageIndex = -1;

            Combo = 0;
            Time = 0f;
        }
    }
}