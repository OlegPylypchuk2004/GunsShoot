using BlasterSystem;
using CameraManagment;
using CurrencyManagment;
using SaveSystem;
using ShopSystem;
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

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
        [SerializeField] private TextMeshProUGUI _nameTextMesh;
        [SerializeField] private TextMeshProUGUI _priceTextMesh;
        [SerializeField] private Image _priceCurrencyIconImage;
        [SerializeField] private TextMeshProUGUI _buyButtonTextMesh;

        private CurrencyWallet _currencyWallet;
        private ShopFrame[] _shopFrames;
        private BlasterConfig _selectedBlasterConfig;
        private PreviewBlaster _currentPreviewBlaster;
        private BlasterConfig[] _blasterConfigs;

        public event Action<PreviewBlaster> PreviewBlasterChanged;

        [Inject]
        private void Construct(CurrencyWallet currencyWallet)
        {
            _currencyWallet = currencyWallet;
        }

        private void Awake()
        {
            _blasterConfigs = LoadBlasterConfigs();

            UpdateSelectedBlaster(_blasterConfigs);
            SpawnShopFrames(_blasterConfigs);
        }

        private void OnEnable()
        {
            foreach (ShopFrame shopFrame in _shopFrames)
            {
                shopFrame.Selected += OnShopFrameSelected;
            }

            _backButton.onClick.AddListener(OnBackButtonClicked);
            _buyButton.onClick.AddListener(OnBuyButtonClicked);

            SpawnPreviewBlaster();
            UpdateDisplay();
        }

        private void OnDisable()
        {
            foreach (ShopFrame shopFrame in _shopFrames)
            {
                shopFrame.Selected -= OnShopFrameSelected;
            }

            _backButton.onClick.RemoveListener(OnBackButtonClicked);
            _buyButton.onClick.RemoveListener(OnBuyButtonClicked);
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

        private void OnBuyButtonClicked()
        {
            if (_selectedBlasterConfig == null)
            {
                return;
            }

            if (SaveManager.Data.IsBlasterPurchased(_selectedBlasterConfig))
            {
                return;
            }

            if (_currencyWallet.TryReduce(new WalletOperationData(_selectedBlasterConfig.Price.CurrencyConfig, _selectedBlasterConfig.Price.Count)))
            {
                SaveManager.Data.Blasters.Add(_selectedBlasterConfig.ID, 1);
                SaveManager.Data.SelectedBlasterID = _selectedBlasterConfig.ID;
                SaveManager.Save();

                UpdateDisplay();
            }
        }

        private void SpawnShopFrames(BlasterConfig[] blasterConfigs)
        {
            _shopFrames = new ShopFrame[blasterConfigs.Length];

            for (int i = 0; i < blasterConfigs.Length; i++)
            {
                _shopFrames[i] = Instantiate(_shopFramePrefab, _shopFramesParent);
                _shopFrames[i].Initialize(blasterConfigs[i]);

                bool isBlasterPuchased = SaveManager.Data.IsBlasterPurchased(_shopFrames[i].BlasterConfig);
                string selectedBlasterID = SaveManager.Data.SelectedBlasterID;
                bool isSelected = isBlasterPuchased && _shopFrames[i].BlasterConfig.ID == selectedBlasterID;

                _shopFrames[i].UpdateDisplay(isBlasterPuchased, isSelected);
            }
        }

        private BlasterConfig[] LoadBlasterConfigs()
        {
            return Resources.LoadAll<BlasterConfig>("Configs/Blasters")
                .OrderBy(blasterConfig => blasterConfig.Type)
                .ToArray();
        }

        private void SpawnPreviewBlaster()
        {
            if (_selectedBlasterConfig == null)
            {
                return;
            }

            if (_currentPreviewBlaster != null)
            {
                Destroy(_currentPreviewBlaster.gameObject);
            }

            _currentPreviewBlaster = Instantiate(_selectedBlasterConfig.PreviewPrefab, _orbitCamera.transform);

            PreviewBlasterChanged?.Invoke(_currentPreviewBlaster);
        }

        private void OnShopFrameSelected(BlasterConfig blasterConfig)
        {
            _selectedBlasterConfig = blasterConfig;

            SelectBlaster();
            SpawnPreviewBlaster();
            UpdateDisplay();
        }

        private void SelectBlaster()
        {
            if (_selectedBlasterConfig == null)
            {
                return;
            }

            if (!SaveManager.Data.IsBlasterPurchased(_selectedBlasterConfig))
            {
                return;
            }

            SaveManager.Data.SelectedBlasterID = _selectedBlasterConfig.ID;
            SaveManager.Save();
        }

        private void UpdateDisplay()
        {
            UpdateShopFramesDisplay();
            UpdatePreviewBlaster();
            UpdatePriceDisplay();
            UpdateBuyButtonDisplay();
        }

        private void UpdateSelectedBlaster(BlasterConfig[] blasterConfigs)
        {
            if (_selectedBlasterConfig != null)
            {
                return;
            }

            string selectedBlasterID = SaveManager.Data.SelectedBlasterID;
            bool isSelectedBlasterIDFound = false;

            foreach (BlasterConfig blasterConfig in blasterConfigs)
            {
                if (blasterConfig.ID == selectedBlasterID)
                {
                    if (SaveManager.Data.IsBlasterPurchased(blasterConfig))
                    {
                        _selectedBlasterConfig = blasterConfig;
                        isSelectedBlasterIDFound = true;

                        break;
                    }
                }
            }

            if (!isSelectedBlasterIDFound && _shopFrames != null && _shopFrames.Length > 0)
            {
                SaveManager.Data.SelectedBlasterID = _shopFrames[0].BlasterConfig.ID;
            }
        }

        private void UpdateShopFramesDisplay()
        {
            string selectedBlasterID = SaveManager.Data.SelectedBlasterID;

            foreach (ShopFrame shopFrame in _shopFrames)
            {
                bool isBlasterPurchased = SaveManager.Data.IsBlasterPurchased(shopFrame.BlasterConfig);
                bool isSelected = isBlasterPurchased && shopFrame.BlasterConfig.ID == selectedBlasterID;

                shopFrame.UpdateDisplay(isBlasterPurchased, isSelected);
            }
        }

        private void UpdatePreviewBlaster()
        {
            if (_selectedBlasterConfig == null || _currentPreviewBlaster == null)
            {
                return;
            }

            _currentPreviewBlaster.UpdateDisplay(SaveManager.Data.IsBlasterPurchased(_selectedBlasterConfig));
        }

        private void UpdatePriceDisplay()
        {
            if (_selectedBlasterConfig == null)
            {
                return;
            }

            _nameTextMesh.text = _selectedBlasterConfig.DisplayName;
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