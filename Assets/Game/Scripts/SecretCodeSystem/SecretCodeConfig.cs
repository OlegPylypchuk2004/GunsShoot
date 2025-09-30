using CurrencyManagment;
using UnityEngine;

namespace SecretCodeSystem
{
    [CreateAssetMenu(fileName = "SecretCodeConfig", menuName = "Configs/SecretCode")]
    public class SecretCodeConfig : ScriptableObject
    {
        [field: SerializeField] public string Key { get; private set; }
        [field: SerializeField] public WalletOperationData Reward { get; private set; }
    }
}