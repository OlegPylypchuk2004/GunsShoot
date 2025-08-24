using SaveSystem;
using SceneManagment;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Menu.SectionSystem
{
    public class SettingsSection : Section
    {
        [Space(10f), SerializeField] private Button _backButton;
        [SerializeField] private Section _previousSection;
        [SerializeField] private Button _secretCodeButton;
        [SerializeField] private Button _resetProgressButton;
        [SerializeField] private GameObject _fpsDisplay;

        private SceneLoader _sceneLoader;

        [Inject]
        private void Construct(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private void OnEnable()
        {
            _fpsDisplay.SetActive(!_fpsDisplay.activeInHierarchy);

            _backButton.onClick.AddListener(OnBackButtonClicked);
            _secretCodeButton.onClick.AddListener(OnSecretCodeButtonClicked);
            _resetProgressButton.onClick.AddListener(OnResetProgressButtonClicked);
        }

        private void OnDisable()
        {
            _backButton.onClick.RemoveListener(OnBackButtonClicked);
            _secretCodeButton.onClick.RemoveListener(OnSecretCodeButtonClicked);
            _resetProgressButton.onClick.RemoveListener(OnResetProgressButtonClicked);
        }

        private void OnBackButtonClicked()
        {
            _sectionChanger.Change(_previousSection);
        }

        private void OnSecretCodeButtonClicked()
        {

        }

        private void OnResetProgressButtonClicked()
        {
            SaveManager.Delete();
            _sceneLoader.Load(0);
        }
    }
}