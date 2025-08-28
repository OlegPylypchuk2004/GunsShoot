using UnityEngine;

namespace GameModeSystem
{
    public abstract class GameModeConfig : ScriptableObject
    {
        [field: SerializeField] public string ID { get; private set; }
        [field: SerializeField] public string DisplayName { get; private set; }
        [field: SerializeField] public string DisplaySubtitle { get; private set; }
        [field: SerializeField] public int SceneIndex { get; private set; }
        [field: SerializeField] public GameModeType Type { get; private set; }

        [SerializeField] protected string StagesConfigFilePath;

        public abstract string GetStagesConfigFilePath();
    }
}