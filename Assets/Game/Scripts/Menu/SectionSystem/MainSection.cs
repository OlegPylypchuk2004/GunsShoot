using GameModeSystem;
using Global;
using SceneManagment;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Menu.SectionSystem
{
    public class MainSection : Section
    {
        [Space(10f), SerializeField] private Button _shopButton;
        [SerializeField] private ShopSection _shopSection;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private SettingsSection _settingsSection;
        [SerializeField] private Button[] _marketButtons;
        [SerializeField] private MarketSection _marketSection;
        [SerializeField] private GameModeButton[] _gameModeButtons;

        private SceneLoader _sceneLoader;

        [Inject]
        private void Construct(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private void OnEnable()
        {
            _shopButton.onClick.AddListener(OnShopButtonClicked);
            _settingsButton.onClick.AddListener(OnSettingsButtonClicked);

            foreach (Button marketButton in _marketButtons)
            {
                marketButton.onClick.AddListener(OnMarketButtonClicked);
            }

            foreach (GameModeButton gameModeButton in _gameModeButtons)
            {
                gameModeButton.Selected += OnGameModeSelected;
            }
        }

        private void OnDisable()
        {
            _shopButton.onClick.RemoveListener(OnShopButtonClicked);
            _settingsButton.onClick.RemoveListener(OnSettingsButtonClicked);

            foreach (Button marketButton in _marketButtons)
            {
                marketButton.onClick.RemoveListener(OnMarketButtonClicked);
            }

            foreach (GameModeButton gameModeButton in _gameModeButtons)
            {
                gameModeButton.Selected -= OnGameModeSelected;
            }
        }

        private void OnShopButtonClicked()
        {
            _sectionChanger.Change(_shopSection);
        }

        private void OnSettingsButtonClicked()
        {
            _sectionChanger.Change(_settingsSection);
        }

        private void OnMarketButtonClicked()
        {
            _sectionChanger.Change(_marketSection);
        }

        private void OnGameModeSelected(GameModeConfig gameModeConfig)
        {
            if (gameModeConfig == null)
            {
                return;
            }

            LocalGameData.GameModeConfig = gameModeConfig;
            _sceneLoader.Load(gameModeConfig.SceneIndex);
        }
    }
}