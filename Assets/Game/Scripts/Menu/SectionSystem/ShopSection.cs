using BlasterSystem;
using CameraManagment;
using SaveSystem;
using ShopSystem;
using System;
using System.Linq;
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
                shopFrame.UpdateDisplay(IsBlasterBought(shopFrame.BlasterConfig));

                shopFrame.Selected += OnShopFrameSelected;
            }

            _backButton.onClick.AddListener(OnBackButtonClicked);
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
                _shopFrames[i].UpdateDisplay(IsBlasterBought(blasterConfigs[i]));
            }

            //Test spawn fake buttons to test UI

            for (int i = 0; i < 24; i++)
            {
                Instantiate(_shopFramePrefab, _shopFramesParent);
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
        }

        private void SpawnPreviewBlaster()
        {
            _currentPreviewBlaster = Instantiate(_selectedBlasterConfig.PreviewPrefab, _orbitCamera.transform);

            PreviewBlasterChanged?.Invoke(_currentPreviewBlaster);
        }

        private bool IsBlasterBought(BlasterConfig blasterConfig)
        {
            return SaveManager.Data.Blasters.Any(blaster => blaster.ID == blasterConfig.ID);
        }
    }
}