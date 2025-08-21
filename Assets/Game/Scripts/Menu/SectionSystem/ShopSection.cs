using BlasterSystem;
using CameraManagment;
using SaveSystem;
using ShopSystem;
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.SectionSystem
{
    public class ShopSection : Section
    {
        [Space(10f), SerializeField] private Button _backButton;
        [SerializeField] private Section _previousSection;
        [SerializeField] private ShopFrame _shopFramePrefab;
        [SerializeField] private RectTransform _shopFramesParent;
        [SerializeField] private OrbitCamera _orbitCamera;
        [SerializeField] private Button _buyButton;
        [SerializeField] private TextMeshProUGUI _priceTextMesh;
        [SerializeField] private Image _priceCurrencyIconImage;
        [SerializeField] private TextMeshProUGUI _buyButtonTextMesh;

        private ShopFrame[] _shopFrames;
        private BlasterConfig _selectedBlasterConfig;
        private PreviewBlaster _currentPreviewBlaster;

        public event Action<PreviewBlaster> PreviewBlasterChanged;

        private void Awake()
        {
            SpawnShopFrames();
        }

        private void OnEnable()
        {
            foreach (ShopFrame shopFrame in _shopFrames)
            {
                shopFrame.UpdateDisplay(SaveManager.Data.IsBlasterPurchased(shopFrame.BlasterConfig));

                shopFrame.Selected += OnShopFrameSelected;
            }

            _backButton.onClick.AddListener(OnBackButtonClicked);

            SpawnPreviewBlaster();
            UpdatePriceDisplay();
            UpdateBuyButtonDisplay();
        }

        private void OnDisable()
        {
            foreach (ShopFrame shopFrame in _shopFrames)
            {
                shopFrame.Selected -= OnShopFrameSelected;
            }

            _backButton.onClick.RemoveListener(OnBackButtonClicked);
        }

        public override void Deactivate()
        {
            base.Deactivate();

            _orbitCamera.SetInteractible(false);
        }

        protected override void OnUIAppeared()
        {
            base.OnUIAppeared();

            _orbitCamera.SetInteractible(true);
        }

        private void OnBackButtonClicked()
        {
            _sectionChanger.Change(_previousSection);
        }

        private void SpawnShopFrames()
        {
            BlasterConfig[] blasterConfigs = LoadBlasterConfigs();
            _shopFrames = new ShopFrame[blasterConfigs.Length];

            for (int i = 0; i < blasterConfigs.Length; i++)
            {
                _shopFrames[i] = Instantiate(_shopFramePrefab, _shopFramesParent);
                _shopFrames[i].Initialize(blasterConfigs[i]);
                _shopFrames[i].UpdateDisplay(SaveManager.Data.IsBlasterPurchased(blasterConfigs[i]));
            }
        }

        private BlasterConfig[] LoadBlasterConfigs()
        {
            return Resources.LoadAll<BlasterConfig>("Configs/Blasters")
                .OrderBy(blasterConfig => blasterConfig.Type)
                .ToArray();
        }

        private void OnShopFrameSelected(BlasterConfig blasterConfig)
        {
            _selectedBlasterConfig = blasterConfig;

            if (_currentPreviewBlaster != null)
            {
                Destroy(_currentPreviewBlaster.gameObject);
            }

            SpawnPreviewBlaster();
            UpdatePriceDisplay();
            UpdateBuyButtonDisplay();
        }

        private void SpawnPreviewBlaster()
        {
            if (_selectedBlasterConfig == null)
            {
                return;
            }

            _currentPreviewBlaster = Instantiate(_selectedBlasterConfig.PreviewPrefab, _orbitCamera.transform);

            PreviewBlasterChanged?.Invoke(_currentPreviewBlaster);
        }

        private void UpdatePriceDisplay()
        {
            if (_selectedBlasterConfig == null)
            {
                return;
            }

            _priceTextMesh.text = $"{_selectedBlasterConfig.Price.Count}";

            _priceCurrencyIconImage.sprite = _selectedBlasterConfig.Price.CurrencyConfig.Icon;
            _priceCurrencyIconImage.SetNativeSize();
        }

        private void UpdateBuyButtonDisplay()
        {
            if (_selectedBlasterConfig == null)
            {
                return;
            }

            bool isBlasterPurchased = SaveManager.Data.IsBlasterPurchased(_selectedBlasterConfig);

            if (isBlasterPurchased)
            {
                _buyButtonTextMesh.text = "Purchased";
            }
            else
            {
                _buyButtonTextMesh.text = "Buy";
            }

            _buyButton.interactable = !isBlasterPurchased;
        }
    }
}