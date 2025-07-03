using UnityEngine;
using UnityEngine.UI;

namespace Menu.SectionSystem
{
    public class MainSection : Section
    {
        [Space(10f), SerializeField] private Button _shopButton;
        [SerializeField] private ShopSection _shopSection;

        private void OnEnable()
        {
            _shopButton.onClick.AddListener(OnShopButtonClicked);
        }

        private void OnDisable()
        {
            _shopButton.onClick.RemoveListener(OnShopButtonClicked);
        }

        private void OnShopButtonClicked()
        {
            _sectionChanger.Change(_shopSection);
        }
    }
}