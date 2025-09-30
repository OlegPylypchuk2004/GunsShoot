using SaveSystem;
using UnityEngine;

namespace GameModeSystem
{
    [CreateAssetMenu(fileName = "GameModeConfig", menuName = "Configs/GameModes/Level")]
    public class LevelGameModeConfig : GameModeConfig
    {
        [field: SerializeField, Min(1)] public int LevelsCount { get; private set; }

        public override string GetStagesConfigFilePath()
        {
            SaveData saveData = SaveManager.Data;

            if (saveData.GameModes.ContainsKey(ID))
            {
                int levelNumber = saveData.GameModes[ID];

                if (levelNumber > LevelsCount)
                {
                    levelNumber = LevelsCount;
                }

                string path = $"{StagesConfigFilePath}{levelNumber}.json";

                return path;
            }
            else
            {
                return $"{StagesConfigFilePath}{1}.json";
            }
        }
    }
}