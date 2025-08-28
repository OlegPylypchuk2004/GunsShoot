using SaveSystem;
using UnityEngine;

namespace GameModeSystem
{
    [CreateAssetMenu(fileName = "GameModeConfig", menuName = "Configs/GameModes/Level")]
    public class LevelGameModeConfig : GameModeConfig
    {
        public override string GetStagesConfigFilePath()
        {
            SaveData saveData = SaveManager.Data;

            if (saveData.GameModes.ContainsKey(ID))
            {
                int completedLevelsCount = saveData.GameModes[ID];
                string path = $"{StagesConfigFilePath}{completedLevelsCount + 1}.json";

                return path;
            }
            else
            {
                return $"{StagesConfigFilePath}{1}.json";
            }
        }
    }
}