using UnityEngine;

namespace CurrencyManagment
{
    [CreateAssetMenu(fileName = "CurrencyConfig", menuName = "Configs/Currency")]
    public class CurrencyConfig : ScriptableObject
    {
        [field: SerializeField] public string ID { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public int MaxCount { get; private set; }
    }
}