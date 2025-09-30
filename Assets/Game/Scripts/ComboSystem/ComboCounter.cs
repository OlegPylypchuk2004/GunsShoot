using BlasterSystem;
using ObstacleSystem;
using System;

namespace ComboSystem
{
    public class ComboCounter
    {
        private ObstacleContainer _obstacleContainer;
        private BlasterHolder _blasterHolder;

        private ComboStageData[] _stages;
        private int _stageIndex;
        private int _cashedStageIndex;
        private int _combo;
        private float _time;

        public event Action<int> ComboChanged;
        public event Action<float> TimeChanged;

        public ComboCounter(ObstacleContainer obstacleContainer, BlasterHolder blasterHolder, ComboConfig comboConfig)
        {
            _obstacleContainer = obstacleContainer;
            _blasterHolder = blasterHolder;

            _stages = comboConfig.Stages;

            _obstacleContainer.ObstacleAdded += OnObstacleAdded;
            _obstacleContainer.ObstacleRemoved += OnObstacleRemoved;

            _stageIndex = -1;
            _cashedStageIndex = 0;
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
                return _time / _stages[_cashedStageIndex].Time;
            }
        }

        public void Update()
        {
            if (_time > 0f)
            {
                if (_blasterHolder.Blaster != null && _blasterHolder.Blaster.State != BlasterState.ReadyToShoot)
                {
                    return;
                }

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

            _cashedStageIndex = _stageIndex;

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