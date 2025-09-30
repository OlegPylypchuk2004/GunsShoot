using Global;
using HealthSystem;
using SaveSystem;
using StageSystem;
using System;

namespace GameModeSystem
{
    public class LevelGameMode : IGameMode
    {
        private HealthManager _healthManager;
        private StageManager _stageManager;
        private bool _isCompleted;

        public event Action<bool> GameOver;

        public bool IsCompleted => _isCompleted;

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

        private void OnHealthIsOver()
        {
            GameOver?.Invoke(false);
        }

        private void OnStagesAreOver()
        {
            SaveData();

            _isCompleted = true;

            GameOver?.Invoke(true);
        }

        private void SaveData()
        {
            SaveData saveData = SaveManager.Data;
            string gameModeID = LocalGameData.GameModeConfig.ID;
            int levelNumber = 1;

            if (saveData.GameModes.ContainsKey(gameModeID))
            {
                levelNumber = saveData.GameModes[gameModeID] + 1;

                if (levelNumber > saveData.GameModes[gameModeID])
                {
                    saveData.GameModes[gameModeID] = levelNumber;
                }
            }
            else
            {
                saveData.GameModes.Add(gameModeID, levelNumber);
            }

            SaveManager.Save();
        }
    }
}