using UnityEngine;

namespace ComboSystem
{
    [CreateAssetMenu(fileName = "ComboConfig", menuName = "Configs/Combo")]
    public class ComboConfig : ScriptableObject
    {
        [field: SerializeField] public ComboStageData[] Stages { get; private set; }
    }
}