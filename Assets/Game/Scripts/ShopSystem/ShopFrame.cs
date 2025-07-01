using BlasterSystem;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ShopSystem
{
    public class ShopFrame : MonoBehaviour
    {
        [SerializeField] private Button _button;

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
        }

        private void OnButtonClicked()
        {
            Selected?.Invoke(_blasterConfig);
        }
    }
}