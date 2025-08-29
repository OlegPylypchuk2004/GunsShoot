using UnityEngine;

namespace SecretCodeSystem
{
    [CreateAssetMenu(fileName = "SecretCodeConfig", menuName = "Configs/SecretCode")]
    public class SecretCodeConfig : ScriptableObject
    {
        [field: SerializeField] public string Key { get; private set; }
    }
}