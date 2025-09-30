using CurrencyManagment;
using UnityEngine;

namespace EnergySystem
{
    [CreateAssetMenu(fileName = "EnergySystem", menuName = "Configs/Energy")]
    public class EnergySystemConfig : ScriptableObject
    {
        [field: SerializeField] public CurrencyConfig EnergyCurrencyConfig { get; private set; }
        [field: SerializeField, Min(0f)] public float Delay { get; private set; }
        [field: SerializeField, Min(1)] public int IncreaseValue { get; private set; }
    }
}