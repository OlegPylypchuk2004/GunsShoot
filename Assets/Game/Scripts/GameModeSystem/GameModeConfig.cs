using UnityEngine;

namespace GameModeSystem
{
    [CreateAssetMenu(fileName = "GameModeConfig", menuName = "Configs/GameMode")]
    public class GameModeConfig : ScriptableObject
    {
        [field: SerializeField] public string ID { get; private set; }
        [field: SerializeField] public string DisplayName { get; private set; }
        [field: SerializeField] public int SceneIndex { get; private set; }
        [field: SerializeField] public string StagesConfigFilePath { get; private set; }
    }
}