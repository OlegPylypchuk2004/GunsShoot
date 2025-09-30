using CurrencyManagment;
using EnergySystem;
using SaveSystem;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace EditorTools
{
    public class EditorTool : MonoBehaviour
    {
        [SerializeField] private CurrencyConfig _creditsCurrencyConfig;
        [SerializeField] private CurrencyConfig _energyCurrencyConfig;

        private CurrencyWallet _currencyWallet;
        private EnergyManager _energyManager;

        [Inject]
        private void Construct(CurrencyWallet currencyWallet, EnergyManager energyManager)
        {
            _currencyWallet = currencyWallet;
            _energyManager = energyManager;
        }

        private void Awake()
        {
#if !UNITY_EDITOR
            Destroy(gameObject);
#endif
        }

        private void Start()
        {
            if (long.TryParse(SaveManager.Data.LastExitTime, out long binary1))
            {
                DateTime lastExitTime = DateTime.FromBinary(binary1);
                Debug.Log("Last Exit Time: " + lastExitTime.ToString("HH:mm:ss"));
            }

            if (long.TryParse(SaveManager.Data.EnergyLastRecoveryTime, out long binary2))
            {
                DateTime lastRecoveryTime = DateTime.FromBinary(binary2);
                Debug.Log("Last Recovery Time: " + lastRecoveryTime.ToString("HH:mm:ss"));
            }
        }

        private void OnDestroy()
        {
            _energyManager.Dispose();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                SaveManager.Save();
            }
            else if (Input.GetKeyDown(KeyCode.I))
            {
                SaveManager.LogSavePath();
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                _currencyWallet.TryIncrease(new WalletOperationData(_creditsCurrencyConfig, 1));
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                _currencyWallet.TryIncrease(new WalletOperationData(_energyCurrencyConfig, 1));
            }
            else if (Input.GetKeyDown(KeyCode.M))
            {
                _currencyWallet.TryReduce(new WalletOperationData(_energyCurrencyConfig, 1));
            }
            else if (Input.GetKeyDown(KeyCode.Delete))
            {
                SaveManager.Delete();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}