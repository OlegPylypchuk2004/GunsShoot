using CurrencyManagment;
using SaveSystem;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Menu.SectionSystem
{
    public class RateSection : Section
    {
        [Space(10f), SerializeField] private string _link;
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _rateButton;
        [SerializeField] private GameObject _claimedDisplay;
        [SerializeField] private GameObject _unclaimedDisplay;
        [SerializeField] private WalletOperationData _rewardData;
        [SerializeField] private Section _previousSection;

        private CurrencyWallet _currencyWallet;

        [Inject]
        private void Construct(CurrencyWallet currencyWallet)
        {
            _currencyWallet = currencyWallet;
        }

        private void OnEnable()
        {
            UpdateDisplay();

            _backButton.onClick.AddListener(OnBackButtonClicked);
            _rateButton.onClick.AddListener(OnRateButtonClicked);
        }

        private void OnDisable()
        {
            _backButton.onClick.RemoveListener(OnBackButtonClicked);
            _rateButton.onClick.RemoveListener(OnRateButtonClicked);
        }

        private void UpdateDisplay()
        {
            bool isRewardClaimed = SaveManager.Data.IsRateRewardClaimed;

            _claimedDisplay.gameObject.SetActive(!isRewardClaimed);
            _unclaimedDisplay.gameObject.SetActive(isRewardClaimed);
        }

        private void OnBackButtonClicked()
        {
            _sectionChanger.Change(_previousSection);
        }

        private void OnRateButtonClicked()
        {
            Application.OpenURL(_link);

            if (SaveManager.Data.IsRateRewardClaimed)
            {
                return;
            }

            _currencyWallet.TryIncrease(_rewardData);

            SaveManager.Data.IsRateRewardClaimed = true;
            SaveManager.Save();

            UpdateDisplay();
        }
    }
}