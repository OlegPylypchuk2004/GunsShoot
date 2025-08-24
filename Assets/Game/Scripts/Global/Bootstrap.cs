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

        private CurrencyWallet _currencyWallet;

        [Inject]
        private void Construct(CurrencyWallet currencyWallet)
        {
            _currencyWallet = currencyWallet;
        }

        private void Start()
        {
            AddInitialCurrency();
            AddInitialBlasters();
            SetInitialSelectedBlaster();

            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 120;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        private void AddInitialCurrency()
        {
            SaveData saveData = SaveManager.Data;

            if (saveData.IsInitialCurrencyAdded)
            {
                return;
            }

            saveData.IsInitialCurrencyAdded = true;

            foreach (WalletOperationData initialCurrency in _initialCurrency)
            {
                _currencyWallet.TryIncrease(initialCurrency);
            }
        }

        private void AddInitialBlasters()
        {
            SaveData saveData = SaveManager.Data;

            foreach (BlasterConfig initialBoughtBlaster in _initialBoughtBlasters)
            {
                if (!saveData.IsBlasterPurchased(initialBoughtBlaster))
                {
                    BlasterData blasterData = new BlasterData(initialBoughtBlaster.ID, 1);
                    saveData.Blasters.Add(blasterData);
                    SaveManager.Save();
                }
            }
        }

        private void SetInitialSelectedBlaster()
        {
            SaveData saveData = SaveManager.Data;

            if (string.IsNullOrEmpty(saveData.SelectedBlasterID))
            {
                saveData.SelectedBlasterID = _initialSelectedBlaster.ID;
                SaveManager.Save();
            }
        }
    }
}