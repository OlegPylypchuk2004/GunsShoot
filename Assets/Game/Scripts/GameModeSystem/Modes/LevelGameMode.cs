using HealthSystem;
using System;

namespace GameModeSystem
{
    public class LevelGameMode : IGameMode
    {
        private HealthManager _healthManager;

        public event Action<bool> GameOver;

        public LevelGameMode(HealthManager healthManager)
        {
            _healthManager = healthManager;

            _healthManager.HealthIsOver += OnHealthIsOver;
        }

        ~LevelGameMode()
        {
            _healthManager.HealthIsOver -= OnHealthIsOver;
        }

        public void SaveData()
        {

        }

        private void OnHealthIsOver()
        {
            GameOver?.Invoke(false);
        }
    }
}