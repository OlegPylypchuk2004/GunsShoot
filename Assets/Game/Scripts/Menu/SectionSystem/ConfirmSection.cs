using System;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.SectionSystem
{
    public class ConfirmSection : Section
    {
        [Space(10f), SerializeField] private Button _backButton;
        [SerializeField] private Button _noButton;
        [SerializeField] private Button _yesButton;

        private Section _previousSection;

        public event Action<bool> ChoiseMade;

        private void OnEnable()
        {
            _backButton.onClick.AddListener(OnBackButtonClicked);
            _noButton.onClick.AddListener(OnNoButtonClicked);
            _yesButton.onClick.AddListener(OnYesButtonClicked);
        }

        private void OnDisable()
        {
            _backButton.onClick.RemoveListener(OnBackButtonClicked);
            _noButton.onClick.RemoveListener(OnNoButtonClicked);
            _yesButton.onClick.RemoveListener(OnYesButtonClicked);
        }

        public void SetPreviousSection(Section previousSection)
        {
            _previousSection = previousSection;
        }

        private void OnBackButtonClicked()
        {
            _sectionChanger.Change(_previousSection);
        }

        private void OnNoButtonClicked()
        {
            _sectionChanger.Change(_previousSection);

            ChoiseMade?.Invoke(false);
        }

        private void OnYesButtonClicked()
        {
            ChoiseMade?.Invoke(true);
        }
    }
}