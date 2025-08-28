using UnityEngine;

namespace GameModeSystem
{
    [CreateAssetMenu(fileName = "GameModeConfig", menuName = "Configs/GameModes/Endless")]
    public class EndlessGameModeConfig : GameModeConfig
    {
        public override string GetStagesConfigFilePath()
        {
            return StagesConfigFilePath;
        }
    }
}