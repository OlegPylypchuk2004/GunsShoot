using GameModeSystem;
using UnityEngine;

namespace Global
{
    public static class LocalGameData
    {
        public static GameModeConfig GameModeConfig { get; set; }

        static LocalGameData()
        {
            GameModeConfig = Resources.Load<GameModeConfig>("Configs/GameModes/game_mode_endless_normal");
        }
    }
}