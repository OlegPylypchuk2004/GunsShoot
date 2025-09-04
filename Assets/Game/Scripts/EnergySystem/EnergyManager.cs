using CurrencyManagment;
using System;
using UnityEngine;

namespace EnergySystem
{
    public class EnergyManager : IDisposable
    {
        private CurrencyWallet _currencyWallet;
        private CurrencyConfig _energyCurrencyConfig;
        private float _delay;
        private int _increaseValue;
        private float _currentTime;
        private bool _isEnergyMax;

        public EnergyManager(CurrencyWallet currencyWallet, EnergySystemConfig energySystemConfig)
        {
            _currencyWallet = currencyWallet;
            _energyCurrencyConfig = energySystemConfig.EnergyCurrencyConfig;
            _delay = energySystemConfig.Delay;
            _increaseValue = energySystemConfig.IncreaseValue;

            _isEnergyMax = _currencyWallet.GetCount(_energyCurrencyConfig) >= _energyCurrencyConfig.MaxCount;

            _currencyWallet.CurrencyCountChanged += OnCurrencyCountChanged;
        }

        public void Dispose()
        {
            _currencyWallet.CurrencyCountChanged -= OnCurrencyCountChanged;
        }

        public void Update()
        {
            if (_isEnergyMax)
            {
                return;
            }

            _currentTime += Time.deltaTime;

            if (_currentTime >= _delay)
            {
                ResetTime();

                _currencyWallet.TryIncrease(new WalletOperationData(_energyCurrencyConfig, _increaseValue));
            }
        }

        public void ResetTime()
        {
            _currentTime = 0f;
        }

        private void OnCurrencyCountChanged(WalletOperationData walletOperationData)
        {
            if (walletOperationData.CurrencyConfig != _energyCurrencyConfig)
            {
                return;
            }

            if (_currencyWallet.GetCount(_energyCurrencyConfig) >= _energyCurrencyConfig.MaxCount)
            {
                _isEnergyMax = true;
            }
            else
            {
                if (_isEnergyMax)
                {
                    ResetTime();
                }

                _isEnergyMax = false;
            }
        }
    }
}