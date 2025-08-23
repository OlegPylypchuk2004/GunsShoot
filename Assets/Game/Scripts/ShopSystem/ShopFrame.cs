using BlasterSystem;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ShopSystem
{
    public class ShopFrame : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _iconImage;
        [SerializeField] private Image _checkmark;
        [SerializeField] private GameObject[] _notBoughtDisplayObjects;
        [SerializeField, Min(0f)] private float _checkmarkAppearDuration;
        [SerializeField] private Ease _checkmarkAppearEase;

        private Tween _checkmarkTween;

        public BlasterConfig BlasterConfig { get; private set; }

        public event Action<BlasterConfig> Selected;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
        }

        public void Initialize(BlasterConfig blasterConfig)
        {
            BlasterConfig = blasterConfig;
            _iconImage.sprite = blasterConfig.Icon;
        }

        public void UpdateDisplay(bool isBought, bool isSelected)
        {
            foreach (GameObject notBoughtDisplayObject in _notBoughtDisplayObjects)
            {
                notBoughtDisplayObject.SetActive(!isBought);
            }

            _checkmark.gameObject.SetActive(isSelected);

            if (isSelected)
            {
                PlayAppearCheckmarkTween();
            }
        }

        private void OnButtonClicked()
        {
            Selected?.Invoke(BlasterConfig);
        }

        private Tween PlayAppearCheckmarkTween()
        {
            _checkmarkTween?.Kill();

            if (_checkmark.isActiveAndEnabled)
            {
                _checkmarkTween = _checkmark.transform.DOScale(1f, _checkmarkAppearDuration)
                    .From(0f)
                    .SetEase(_checkmarkAppearEase)
                    .SetLink(gameObject);
            }

            return _checkmarkTween;
        }
    }
}