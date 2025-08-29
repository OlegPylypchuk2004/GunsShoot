using Global;
using SaveSystem;
using SceneManagment;
using UI;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Menu.SectionSystem
{
    public class SettingsSection : Section
    {
        [Space(10f), SerializeField] private Button _backButton;
        [SerializeField] private Section _previousSection;
        [SerializeField] private ToggleButton _soundToggle;
        [SerializeField] private ToggleButton _musicToggle;
        [SerializeField] private ToggleButton _showFPSToggle;
        [SerializeField] private Button _secretCodeButton;
        [SerializeField] private Button _resetProgressButton;
        [SerializeField] private FPSDisplay _fpsDisplay;
        [SerializeField] private SecretCodeSection _secretCodeSection;

        private SceneLoader _sceneLoader;

        [Inject]
        private void Construct(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private void OnEnable()
        {
            UpdateSettings();

            _soundToggle.StateChanged += OnSoundToggleStateChanged;
            _musicToggle.StateChanged += OnMusicToggleStateChanged;
            _showFPSToggle.StateChanged += OnShowFPSToggleStateChanged;

            _backButton.onClick.AddListener(OnBackButtonClicked);
            _secretCodeButton.onClick.AddListener(OnSecretCodeButtonClicked);
            _resetProgressButton.onClick.AddListener(OnResetProgressButtonClicked);
        }

        private void OnDisable()
        {
            _soundToggle.StateChanged -= OnSoundToggleStateChanged;
            _musicToggle.StateChanged -= OnMusicToggleStateChanged;
            _showFPSToggle.StateChanged -= OnShowFPSToggleStateChanged;

            _backButton.onClick.RemoveListener(OnBackButtonClicked);
            _secretCodeButton.onClick.RemoveListener(OnSecretCodeButtonClicked);
            _resetProgressButton.onClick.RemoveListener(OnResetProgressButtonClicked);
        }

        private void UpdateSettings()
        {
            SaveData saveData = SaveManager.Data;

            _soundToggle.Initialize(saveData.IsSoundEnabled);
            _musicToggle.Initialize(saveData.IsMusicEnabled);
            _showFPSToggle.Initialize(saveData.IsShowFPS);
        }

        private void OnSoundToggleStateChanged(bool isEnabled)
        {
            SaveManager.Data.IsSoundEnabled = isEnabled;
            SaveManager.Save();
        }

        private void OnMusicToggleStateChanged(bool isEnabled)
        {
            SaveManager.Data.IsMusicEnabled = isEnabled;
            SaveManager.Save();
        }

        private void OnShowFPSToggleStateChanged(bool isEnabled)
        {
            SaveManager.Data.IsShowFPS = isEnabled;
            SaveManager.Save();

            if (isEnabled)
            {
                _fpsDisplay.Show();
            }
            else
            {
                _fpsDisplay.Hide();
            }
        }

        private void OnBackButtonClicked()
        {
            _sectionChanger.Change(_previousSection);
        }

        private void OnSecretCodeButtonClicked()
        {
            _sectionChanger.Change(_secretCodeSection);
        }

        private void OnResetProgressButtonClicked()
        {
            SaveManager.Delete();
            _sceneLoader.Load(0);
        }
    }
}