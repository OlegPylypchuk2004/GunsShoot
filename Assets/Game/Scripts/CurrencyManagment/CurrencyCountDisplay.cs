using DG.Tweening;
using TMPro;
using UnityEngine;
using VContainer;

namespace CurrencyManagment
{
    public class CurrencyCountDisplay : MonoBehaviour
    {
        [SerializeField] protected CurrencyConfig _currencyConfig;
        [SerializeField] protected TextMeshProUGUI _textMesh;
        [SerializeField, Min(0f)] private float _animationDuration;
        [SerializeField] private Ease _animationEase;

        private CurrencyWallet _currencyWallet;
        private int _currentDisplayedAmount;

        [Inject]
        private void Construct(CurrencyWallet curencyWallet)
        {
            _currencyWallet = curencyWallet;
        }

        private void Start()
        {
            _currencyWallet.CurrencyCountChanged += OnCurrencyAmountChanged;

            _currentDisplayedAmount = _currencyWallet.GetCount(_currencyConfig);
            UpdateTextMesh($"{_currentDisplayedAmount}");
        }

        private void OnDestroy()
        {
            _currencyWallet.CurrencyCountChanged -= OnCurrencyAmountChanged;
        }

        private void OnCurrencyAmountChanged(WalletOperationData walletOperationData)
        {
            if (walletOperationData.CurrencyConfig != _currencyConfig)
            {
                return;
            }

            DOTween.To(() => _currentDisplayedAmount, value => { _currentDisplayedAmount = value; UpdateTextMesh($"{value}"); }, walletOperationData.Count, _animationDuration)
                .SetEase(_animationEase)
                .SetLink(gameObject);
        }

        private void UpdateTextMesh(string text)
        {
            string result = text;

            if (_currencyConfig.MaxCount > 0)
            {
                result += $"/{_currencyConfig.MaxCount}";
            }

            _textMesh.text = result;
        }
    }
}