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

        public event Action<bool> GameOver;

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

        public void SaveData()
        {
            string gameModeID = LocalGameData.GameModeConfig.ID;
            int score = _scoreCounter.Score;
            SaveData saveData = SaveManager.Data;

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

        private void OnHealthIsOver()
        {
            GameOver?.Invoke(false);
        }
    }
}