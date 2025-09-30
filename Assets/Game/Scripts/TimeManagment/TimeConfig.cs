using UnityEngine;

namespace TimeManagment
{
    [CreateAssetMenu(fileName = "TimeConfig", menuName = "Configs/Time")]
    public class TimeConfig : ScriptableObject
    {
        [field: SerializeField, Range(0f, 1f)] public float SlowDownValue { get; private set; }
    }
}