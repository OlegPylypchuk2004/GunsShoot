using UnityEngine;

namespace TimeManagment
{
    [CreateAssetMenu(fileName = "TimeConfig", menuName = "Configs/Time")]
    public class TimeConfig : ScriptableObject
    {
        [field: SerializeField] public float SlowDownValue { get; private set; }
    }
}