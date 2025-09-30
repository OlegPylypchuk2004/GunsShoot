using BlasterSystem;
using CurrencyManagment;
using DG.Tweening;
using GameModeSystem;
using SaveSystem;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VContainer;

namespace Global
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private WalletOperationData[] _initialCurrency;
        [SerializeField] private BlasterConfig[] _initialBoughtBlasters;
        [SerializeField] private BlasterConfig _initialSelectedBlaster;
        [SerializeField] private Image _backgroundImage;
        [SerializeField, Min(0f)] private float _backgroundImageDisappearDuration;
        [SerializeField] private Ease _backgroundImageDisappearEase;

        private SaveData _saveData;
        private CurrencyWallet _currencyWallet;

        [Inject]
        private void Construct(CurrencyWallet currencyWallet)
        {
            _currencyWallet = currencyWallet;
        }

        private void Start()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 120;

            _saveData = SaveManager.Data;

            if (_saveData.IsFirstEntry)
            {
                AddInitialCurrency();
                AddInitialBlasters();
                SetInitialSelectedBlaster();
                InitializeGameModes();
                SetInitialSettings();
                SetInitialTime();

                _saveData.IsFirstEntry = false;

                SaveManager.Save();
            }

            DisappearBackground()
                .OnKill(() =>
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                });
        }

        private void AddInitialCurrency()
        {
            foreach (WalletOperationData initialCurrency in _initialCurrency)
            {
                _currencyWallet.TryIncrease(initialCurrency);
            }
        }

        private void AddInitialBlasters()
        {
            foreach (BlasterConfig initialBoughtBlaster in _initialBoughtBlasters)
            {
                if (!_saveData.IsBlasterPurchased(initialBoughtBlaster))
                {
                    _saveData.Blasters.Add(initialBoughtBlaster.ID, 1);
                }
            }
        }

        private void SetInitialSelectedBlaster()
        {
            if (string.IsNullOrEmpty(_saveData.SelectedBlasterID))
            {
                _saveData.SelectedBlasterID = _initialSelectedBlaster.ID;
            }
        }

        private void InitializeGameModes()
        {
            GameModeConfig[] gameModeConfigs = Resources.LoadAll<GameModeConfig>("Configs/GameModes");

            foreach (GameModeConfig gameModeConfig in gameModeConfigs)
            {
                int value = 0;

                if (gameModeConfig.Type == GameModeType.Level)
                {
                    value = 1;
                }

                _saveData.GameModes.Add(gameModeConfig.ID, value);
            }
        }

        private void SetInitialSettings()
        {
            _saveData.IsSoundEnabled = true;
            _saveData.IsMusicEnabled = true;
            _saveData.IsShowFPS = false;
        }

        private void SetInitialTime()
        {
            DateTime nowTime = DateTime.UtcNow;

            SaveManager.Data.LastExitTime = nowTime.ToBinary().ToString();
            SaveManager.Data.EnergyLastRecoveryTime = nowTime.ToBinary().ToString();
        }

        private Tween DisappearBackground()
        {
            _backgroundImage.gameObject.SetActive(true);

            return _backgroundImage.DOFade(0f, _backgroundImageDisappearDuration)
                .SetEase(_backgroundImageDisappearEase)
                .SetLink(gameObject);
        }
    }
}