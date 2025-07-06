using GameModeSystem;
using UnityEngine;

namespace StageSystem
{
    public class StageManager
    {
        private GameModeConfig _gameModeConfig;

        public int StageNumber { get; private set; }

        public StageData StageData
        {
            get
            {
                int stageIndex = StageNumber - 1;

                if (stageIndex > _gameModeConfig.Stages.Length - 1)
                {
                    stageIndex = _gameModeConfig.Stages.Length - 1;
                }

                return _gameModeConfig.Stages[stageIndex];
            }
        }

        public StageManager()
        {
            StageNumber = 1;
            _gameModeConfig = Resources.Load<GameModeConfig>("Configs/GameModes/game_mode_classic");
        }

        public void IncreaseStageNumber()
        {
            StageNumber++;
        }
    }
}