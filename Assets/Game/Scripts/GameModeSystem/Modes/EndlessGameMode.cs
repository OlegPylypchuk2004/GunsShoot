using Global;
using SaveSystem;
using ScoreSystem;

namespace GameModeSystem.Modes
{
    public class EndlessGameMode : IGameMode
    {
        private ScoreCounter _scoreCounter;

        public EndlessGameMode(ScoreCounter scoreCounter)
        {
            _scoreCounter = scoreCounter;
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
    }
}