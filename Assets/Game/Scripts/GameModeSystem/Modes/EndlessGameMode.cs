using Global;
using HealthSystem;
using SaveSystem;
using ScoreSystem;
using System;

namespace GameModeSystem.Modes
{
    public class EndlessGameMode : IGameMode
    {
        private HealthManager _healthManager;
        private ScoreCounter _scoreCounter;
        private bool _isCompleted;

        public event Action<bool> GameOver;

        public bool IsCompleted => _isCompleted;

        public EndlessGameMode(HealthManager healthManager, ScoreCounter scoreCounter)
        {
            _healthManager = healthManager;
            _scoreCounter = scoreCounter;

            _healthManager.HealthIsOver += OnHealthIsOver;
        }

        ~EndlessGameMode()
        {
            _healthManager.HealthIsOver -= OnHealthIsOver;
        }

        private void OnHealthIsOver()
        {
            SaveData();

            GameOver?.Invoke(false);
        }

        private void SaveData()
        {
            SaveData saveData = SaveManager.Data;
            string gameModeID = LocalGameData.GameModeConfig.ID;
            int score = _scoreCounter.Score;

            if (saveData.GameModes.ContainsKey(gameModeID))
            {
                if (score > saveData.GameModes[gameModeID])
                {
                    saveData.GameModes[gameModeID] = score;
                }
            }
            else
            {
                saveData.GameModes.Add(gameModeID, score);
            }

            SaveManager.Save();
        }
    }
}