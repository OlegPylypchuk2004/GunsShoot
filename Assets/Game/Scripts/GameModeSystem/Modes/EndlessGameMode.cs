using Global;
using SaveSystem;

namespace GameModeSystem.Modes
{
    public class EndlessGameMode : IGameMode
    {
        public void SaveData()
        {
            string gameModeID = LocalGameData.GameModeConfig.ID;
            SaveData saveData = SaveManager.Data;

            if (saveData.GameModes.ContainsKey(gameModeID))
            {
                saveData.GameModes[gameModeID] = 100;
            }
            else
            {
                saveData.GameModes.Add(gameModeID, 50);
            }

            SaveManager.Save();
        }
    }
}