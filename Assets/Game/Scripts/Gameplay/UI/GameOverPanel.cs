using GameModeSystem;
using Global;
using SaveSystem;
using SceneManagment;
using ScoreSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Gameplay.UI
{
    public class GameOverPanel : Panel
    {
        [SerializeField] private Button _tryAgainButton;
        [SerializeField] private Button _continueButton;
        [SerializeField, Min(0)] private int _menuSceneIndex;
        [SerializeField] private TMP_Text _gameModeDisplayTextMesh;
        [SerializeField] private TMP_Text _resultDisplayTextMesh;

        private SceneLoader _sceneLoader;
        private ScoreCounter _scoreCounter;

        [Inject]
        private void Construct(SceneLoader sceneLoader, ScoreCounter scoreCounter)
        {
            _sceneLoader = sceneLoader;
            _scoreCounter = scoreCounter;
        }

        private void Start()
        {
            GameModeConfig gameModeConfig = LocalGameData.GameModeConfig;

            switch (gameModeConfig.Type)
            {
                case GameModeType.Endless:

                    _gameModeDisplayTextMesh.text = $"{gameModeConfig.DisplayName}({gameModeConfig.DisplaySubtitle})";
                    _resultDisplayTextMesh.text = $"{_scoreCounter.Score}";

                    break;

                case GameModeType.Level:

                    _gameModeDisplayTextMesh.text = $"{gameModeConfig.DisplayName}";

                    int levelNumber = 1;
                    SaveData saveData = SaveManager.Data;

                    if (saveData.GameModes.ContainsKey(gameModeConfig.ID))
                    {
                        levelNumber = saveData.GameModes[gameModeConfig.ID];
                    }

                    _resultDisplayTextMesh.text = $"{levelNumber}";

                    break;
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            _tryAgainButton.onClick.AddListener(OnTryAgainButtonClicked);
            _continueButton.onClick.AddListener(OnContinueButtonClicked);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _tryAgainButton.onClick.RemoveListener(OnTryAgainButtonClicked);
            _continueButton.onClick.RemoveListener(OnContinueButtonClicked);
        }

        private void OnTryAgainButtonClicked()
        {
            _sceneLoader.Load(_sceneLoader.CurrentSceneIndex);
        }

        private void OnContinueButtonClicked()
        {
            _sceneLoader.Load(_menuSceneIndex);
        }
    }
}