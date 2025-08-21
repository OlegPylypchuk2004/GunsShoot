using System;
using UnityEngine;

namespace CurrencyManagment
{
    [Serializable]
    public class WalletOperationData
    {
        [field: SerializeField] public CurrencyConfig CurrencyConfig { get; private set; }
        [field: SerializeField] public int Count { get; private set; }

        public WalletOperationData(CurrencyConfig currencyConfig, int count)
        {
            CurrencyConfig = currencyConfig;
            Count = count;
        }
    }
}