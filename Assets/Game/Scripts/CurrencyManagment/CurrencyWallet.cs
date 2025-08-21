using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CurrencyManagment
{
    public class CurrencyWallet
    {
        private CurrencyConfig[] _currency;

        public IReadOnlyList<CurrencyConfig> Currencies => _currency;

        public event Action<WalletOperationData> CurrencyCountChanged;
        public event Action<WalletOperationData, int> CurrencyIncreased;
        public event Action<WalletOperationData> CurrencyReduced;

        public CurrencyWallet()
        {
            CurrencyConfig[] currencyConfigs = Resources.LoadAll<CurrencyConfig>("Configs/Currency");

            if (currencyConfigs.Length == 0)
            {
                throw new Exception("No currencies configs found.");
            }

            _currency = new CurrencyConfig[currencyConfigs.Length];

            for (int i = 0; i < currencyConfigs.Length; i++)
            {
                _currency[i] = currencyConfigs[i];
            }
        }

        public int GetCount(CurrencyConfig currencyConfig)
        {
            if (_currency.Contains(currencyConfig))
            {
                return PlayerPrefs.GetInt($"{currencyConfig.ID}Count", 0);
            }

            throw new Exception($"Currency: {currencyConfig.ID} not found.");
        }

        public bool TryAddCount(WalletOperationData operationData)
        {
            if (operationData.Count < 0)
            {
                return false;
            }

            if (_currency.Contains(operationData.CurrencyConfig))
            {
                int currentCount = PlayerPrefs.GetInt($"{operationData.CurrencyConfig.ID}Count", 0);
                int newCount = currentCount + operationData.Count;
                int maxCount = operationData.CurrencyConfig.MaxCount;

                if (maxCount > 0 && newCount > maxCount)
                {
                    newCount = maxCount;
                }

                PlayerPrefs.SetInt($"{operationData.CurrencyConfig.ID}Count", newCount);

                CurrencyCountChanged?.Invoke(new WalletOperationData(operationData.CurrencyConfig, newCount));
                CurrencyIncreased?.Invoke(new WalletOperationData(operationData.CurrencyConfig, newCount), operationData.Count);

                return true;
            }
            else
            {
                throw new Exception($"Currency: {operationData.CurrencyConfig.ID} not found.");
            }
        }

        public bool TryReduceCount(WalletOperationData operationData)
        {
            if (operationData.Count < 0)
            {
                return false;
            }

            int currentCount = PlayerPrefs.GetInt($"{operationData.CurrencyConfig.ID}Count", 0);

            if (currentCount < operationData.Count)
            {
                return false;
            }

            if (_currency.Contains(operationData.CurrencyConfig))
            {
                currentCount -= operationData.Count;
                PlayerPrefs.SetInt($"{operationData.CurrencyConfig.ID}Count", currentCount);

                CurrencyCountChanged?.Invoke(new WalletOperationData(operationData.CurrencyConfig, currentCount));
                CurrencyReduced?.Invoke(new WalletOperationData(operationData.CurrencyConfig, operationData.Count));

                return true;
            }
            else
            {
                throw new Exception($"Currency: {operationData.CurrencyConfig.ID} not found.");
            }
        }
    }
}