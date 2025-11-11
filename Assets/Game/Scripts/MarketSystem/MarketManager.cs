using CurrencyManagment;
using UnityEngine;
using VContainer;

namespace MarketSystem
{
    public class MarketManager : MonoBehaviour
    {
        [SerializeField] private CurrencyConfig _currencyConfig;

        private CurrencyWallet _currencyWallet;

        [Inject]
        private void Construct(CurrencyWallet currencyWallet)
        {
            _currencyWallet = currencyWallet;
        }

        public void OnOrderConfirmed(string itemID)
        {
            switch (itemID)
            {
                case "starter_pack_2025":
                    _currencyWallet.TryIncrease(new WalletOperationData(_currencyConfig, 500));
                    break;

                case "medium_pack_2025":
                    _currencyWallet.TryIncrease(new WalletOperationData(_currencyConfig, 2750));
                    break;

                case "big_pack_2025":
                    _currencyWallet.TryIncrease(new WalletOperationData(_currencyConfig, 5500));
                    break;

                case "mega_pack_2025":
                    _currencyWallet.TryIncrease(new WalletOperationData(_currencyConfig, 8250));
                    break;

                case "ultra_pack_2025":
                    _currencyWallet.TryIncrease(new WalletOperationData(_currencyConfig, 12500));
                    break;
            }
        }
    }
}