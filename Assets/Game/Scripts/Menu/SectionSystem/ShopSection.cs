using BlasterSystem;
using DG.Tweening;
using ShopSystem;
using System.Linq;
using UnityEngine;

namespace Menu.SectionSystem
{
    public class ShopSection : Section
    {
        [SerializeField] private ShopFrame _shopFramePrefab;
        [SerializeField] private RectTransform _shopFramesParent;
        [SerializeField] private CanvasGroup _canvasGroup;

        private ShopFrame[] _shopFrames;

        private void Start()
        {
            SpawnShopFrames();

            foreach (ShopFrame shopFrame in _shopFrames)
            {
                shopFrame.Selected += OnShopFrameSelected;
            }
        }

        private void OnDestroy()
        {
            foreach (ShopFrame shopFrame in _shopFrames)
            {
                shopFrame.Selected -= OnShopFrameSelected;
            }
        }

        public override Sequence Appear()
        {
            Sequence sequence = base.Appear();

            sequence.Append(_canvasGroup.DOFade(1f, _appearUIDuration)
                .From(0f)
                .SetEase(_appearUIEase));

            return sequence;
        }

        public override Sequence Disappear()
        {
            Sequence sequence = base.Disappear();

            sequence.Append(_canvasGroup.DOFade(1f, _disappearUIDuration)
                .SetEase(_disappearUIEase));

            return sequence;
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