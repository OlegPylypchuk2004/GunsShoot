using HealthSystem;
using StageSystem;
using System;

namespace GameModeSystem
{
    public class LevelGameMode : IGameMode
    {
        private HealthManager _healthManager;
        private StageManager _stageManager;

        public event Action<bool> GameOver;

        public LevelGameMode(HealthManager healthManager, StageManager stageManager)
        {
            _healthManager = healthManager;
            _stageManager = stageManager;

            _healthManager.HealthIsOver += OnHealthIsOver;
            _stageManager.StagesAreOver += OnStagesAreOver;
        }

        ~LevelGameMode()
        {
            _healthManager.HealthIsOver -= OnHealthIsOver;
            _stageManager.StagesAreOver -= OnStagesAreOver;
        }

        public void SaveData()
        {

        }

        private void OnHealthIsOver()
        {
            GameOver?.Invoke(false);
        }

        private void OnStagesAreOver()
        {
            GameOver?.Invoke(true);
        }
    }
}