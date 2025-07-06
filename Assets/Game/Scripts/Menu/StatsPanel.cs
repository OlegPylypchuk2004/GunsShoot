using BlasterSystem;
using Menu.SectionSystem;
using UnityEngine;

namespace Menu
{
    public class StatsPanel : MonoBehaviour
    {
        [SerializeField] private StatData _damageStatData;
        [SerializeField] private StatData _ammoStatData;
        [SerializeField] private StatData _cooldownStatData;
        [SerializeField] private StatDisplay _damageStatDisplay;
        [SerializeField] private StatDisplay _ammoAmountStatDisplay;
        [SerializeField] private StatDisplay _coodownStatDisplay;
        [SerializeField] private ShopSection _shopSection;

        private void OnEnable()
        {
            _shopSection.PreviewBlasterChanged += OnPreviewBlasterChanged;
        }

        private void OnDisable()
        {
            _shopSection.PreviewBlasterChanged += OnPreviewBlasterChanged;
        }

        private void LateUpdate()
        {
            transform.rotation = Quaternion.identity;
        }

        private void OnPreviewBlasterChanged(PreviewBlaster previewBlaster)
        {
            BlasterConfig blasterConfig = previewBlaster.Config;

            _damageStatDisplay.SetValue(_damageStatData, blasterConfig.Damage);
            _ammoAmountStatDisplay.SetValue(_ammoStatData, blasterConfig.AmmoAmount);
            _coodownStatDisplay.SetValue(_cooldownStatData, blasterConfig.ShotCooldown);
        }
    }
}