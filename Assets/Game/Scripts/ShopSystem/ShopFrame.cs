using BlasterSystem;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ShopSystem
{
    public class ShopFrame : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _iconImage;
        [SerializeField] private GameObject[] _notBoughtDisplayObjects;

        private BlasterConfig _blasterConfig;

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
            _blasterConfig = blasterConfig;
            _iconImage.sprite = blasterConfig.Icon;
        }

        public void UpdateDisplay(bool isBought)
        {
            foreach (GameObject notBoughtDisplayObject in _notBoughtDisplayObjects)
            {
                notBoughtDisplayObject.SetActive(!isBought);
            }
        }

        private void OnButtonClicked()
        {
            Selected?.Invoke(_blasterConfig);
        }
    }
}