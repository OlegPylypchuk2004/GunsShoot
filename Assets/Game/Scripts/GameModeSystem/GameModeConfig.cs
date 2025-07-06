using UnityEngine;

namespace GameModeSystem
{
    [CreateAssetMenu(fileName = "GameModeConfig", menuName = "Configs/GameMode")]
    public class GameModeConfig : ScriptableObject
    {
        [field: SerializeField] public StageData[] Stages { get; private set; }
    }
}