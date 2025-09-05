using CurrencyManagment;
using Global;
using SaveSystem;
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
        private float _recoveryTime;
        private bool _isEnergyMax;

        public event Action<float> RecoveryTimeChanged;

        public float RecoveryTime
        {
            get
            {
                return _recoveryTime;
            }
            private set
            {
                _recoveryTime = value;

                RecoveryTimeChanged?.Invoke(_recoveryTime);
            }
        }

        public EnergyManager(CurrencyWallet currencyWallet, EnergySystemConfig energySystemConfig, TimeTracker timeTracker)
        {
            _currencyWallet = currencyWallet;
            _energyCurrencyConfig = energySystemConfig.EnergyCurrencyConfig;
            _delay = energySystemConfig.Delay;
            _increaseValue = energySystemConfig.IncreaseValue;
            _isEnergyMax = _currencyWallet.GetCount(_energyCurrencyConfig) >= _energyCurrencyConfig.MaxCount;

            RestoreEnergyFromOffline();

            _currencyWallet.CurrencyCountChanged += OnCurrencyCountChanged;
        }

        public void Dispose()
        {
            _currencyWallet.CurrencyCountChanged -= OnCurrencyCountChanged;
            SaveLastRecoveryTime();
        }

        public void Update()
        {
            if (_isEnergyMax)
            {
                return;
            }

            RecoveryTime -= Time.deltaTime;

            if (_recoveryTime <= 0f)
            {
                ResetTime();
                _currencyWallet.TryIncrease(new WalletOperationData(_energyCurrencyConfig, _increaseValue));
                SaveLastRecoveryTime();
            }
        }

        private void RestoreEnergyFromOffline()
        {
            if (_isEnergyMax)
            {
                return;
            }

            DateTime now = DateTime.UtcNow;
            DateTime lastRecovery = LoadLastRecoveryTime();

            if (lastRecovery == DateTime.MinValue)
            {
                ResetTime();

                return;
            }

            TimeSpan passed = now - lastRecovery;
            int cycles = Mathf.FloorToInt((float)(passed.TotalSeconds / _delay));
            double leftover = passed.TotalSeconds % _delay;

            if (cycles > 0)
            {
                _currencyWallet.TryIncrease(new WalletOperationData(_energyCurrencyConfig, cycles * _increaseValue));
            }

            RecoveryTime = _delay - (float)leftover;

            if (_currencyWallet.GetCount(_energyCurrencyConfig) >= _energyCurrencyConfig.MaxCount)
            {
                _isEnergyMax = true;
            }
        }

        private void SaveLastRecoveryTime()
        {
            DateTime lastRecoveryMoment = DateTime.UtcNow.AddSeconds(-RecoveryTime + _delay);
            SaveManager.Data.EnergyLastRecoveryTime = lastRecoveryMoment.ToBinary().ToString();
            SaveManager.Save();
        }

        private DateTime LoadLastRecoveryTime()
        {
            string saved = SaveManager.Data.EnergyLastRecoveryTime;

            if (long.TryParse(saved, out long binary))
            {
                return DateTime.FromBinary(binary);
            }

            return DateTime.MinValue;
        }

        public void ResetTime()
        {
            RecoveryTime = _delay;
            SaveLastRecoveryTime();
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