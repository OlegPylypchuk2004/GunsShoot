using BlasterSystem;
using ShopSystem;
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

        private ShopFrame[] _shopFrames;

        private void OnEnable()
        {
            _backButton.onClick.AddListener(OnBackButtonClicked);
        }

        private void Start()
        {
            SpawnShopFrames();

            foreach (ShopFrame shopFrame in _shopFrames)
            {
                shopFrame.Selected += OnShopFrameSelected;
            }
        }

        private void OnDisable()
        {
            _backButton.onClick.RemoveListener(OnBackButtonClicked);
        }

        private void OnDestroy()
        {
            foreach (ShopFrame shopFrame in _shopFrames)
            {
                shopFrame.Selected -= OnShopFrameSelected;
            }
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

        }
    }
}