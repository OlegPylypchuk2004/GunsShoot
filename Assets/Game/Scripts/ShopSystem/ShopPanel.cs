using BlasterSystem;
using System.Linq;
using UnityEngine;

namespace ShopSystem
{
    public class ShopPanel : MonoBehaviour
    {
        [SerializeField] private ShopFrame _shopFramePrefab;
        [SerializeField] private RectTransform _shopFramesParent;

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