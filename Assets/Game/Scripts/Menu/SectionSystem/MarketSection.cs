using UnityEngine;
using UnityEngine.UI;

namespace Menu.SectionSystem
{
    public class MarketSection : Section
    {
        [Space(10f), SerializeField] private Button _backButton;
        [SerializeField] private Section _previousSection;

        private void OnEnable()
        {
            _backButton.onClick.AddListener(OnBackButtonClicked);
        }

        private void OnDisable()
        {
            _backButton.onClick.RemoveListener(OnBackButtonClicked);
        }

        private void OnBackButtonClicked()
        {
            _sectionChanger.Change(_previousSection);
        }
    }
}