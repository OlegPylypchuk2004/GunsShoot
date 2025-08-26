using BlasterSystem;
using CurrencyManagment;
using SaveSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace Global
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private WalletOperationData[] _initialCurrency;
        [SerializeField] private BlasterConfig[] _initialBoughtBlasters;
        [SerializeField] private BlasterConfig _initialSelectedBlaster;

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
                SetInitialSettings();

                _saveData.IsFirstEntry = false;
                SaveManager.Save();
            }

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
                    SaveManager.Save();
                }
            }
        }

        private void SetInitialSelectedBlaster()
        {
            if (string.IsNullOrEmpty(_saveData.SelectedBlasterID))
            {
                _saveData.SelectedBlasterID = _initialSelectedBlaster.ID;
                SaveManager.Save();
            }
        }

        private void SetInitialSettings()
        {
            _saveData.IsSoundEnabled = true;
            _saveData.IsMusicEnabled = true;
            _saveData.IsShowFPS = false;
        }
    }
}