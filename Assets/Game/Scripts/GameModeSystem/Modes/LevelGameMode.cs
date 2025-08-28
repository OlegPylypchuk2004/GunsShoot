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

        private void OnHealthIsOver()
        {
            GameOver?.Invoke(false);
        }

        private void OnStagesAreOver()
        {
            SaveData();

            GameOver?.Invoke(true);
        }

        private void SaveData()
        {
            string gameModeID = LocalGameData.GameModeConfig.ID;
            int levelNumber = /*LocalGameData.LevelConfig.Number*/100;
            SaveData saveData = SaveManager.Data;

            if (saveData.GameModes.ContainsKey(gameModeID))
            {
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