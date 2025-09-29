using DG.Tweening;
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
        [SerializeField] private TMP_Text _bestResultDisplayTextMesh;

        private SceneLoader _sceneLoader;
        private ScoreCounter _scoreCounter;

        [Inject]
        private void Construct(SceneLoader sceneLoader, ScoreCounter scoreCounter)
        {
            _sceneLoader = sceneLoader;
            _scoreCounter = scoreCounter;
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

        public override Sequence Appear()
        {
            UpdateDisplay();

            return base.Appear();
        }

        private void OnTryAgainButtonClicked()
        {
            _sceneLoader.Load(_sceneLoader.CurrentSceneIndex);
        }

        private void OnContinueButtonClicked()
        {
            _sceneLoader.Load(_menuSceneIndex);
        }

        private void UpdateDisplay()
        {
            UpdateGameModeDisplay();
            UpdateResultDisplay();
            UpdateBestResultDisplay();
        }

        private void UpdateGameModeDisplay()
        {
            GameModeConfig gameModeConfig = LocalGameData.GameModeConfig;

            switch (gameModeConfig.Type)
            {
                case GameModeType.Endless:

                    _gameModeDisplayTextMesh.text = $"{gameModeConfig.DisplayName}({gameModeConfig.DisplaySubtitle})";

                    break;

                case GameModeType.Level:

                    int levelNumber = 1;
                    SaveData saveData = SaveManager.Data;

                    if (saveData.GameModes.ContainsKey(gameModeConfig.ID))
                    {
                        levelNumber = saveData.GameModes[gameModeConfig.ID];
                    }

                    _gameModeDisplayTextMesh.text = $"{gameModeConfig.DisplayName} {levelNumber}";

                    break;
            }
        }

        private void UpdateResultDisplay()
        {
            GameModeConfig gameModeConfig = LocalGameData.GameModeConfig;

            switch (gameModeConfig.Type)
            {
                case GameModeType.Endless:

                    _resultDisplayTextMesh.text = $"{_scoreCounter.Score}";

                    break;

                case GameModeType.Level:

                    _resultDisplayTextMesh.text = $"-";

                    break;
            }
        }

        private void UpdateBestResultDisplay()
        {
            GameModeConfig gameModeConfig = LocalGameData.GameModeConfig;

            switch (gameModeConfig.Type)
            {
                case GameModeType.Endless:

                    int bestResult = _scoreCounter.Score;
                    SaveData saveData = SaveManager.Data;

                    if (saveData.GameModes.ContainsKey(gameModeConfig.ID))
                    {
                        bestResult = saveData.GameModes[gameModeConfig.ID];
                    }

                    _bestResultDisplayTextMesh.text = $"{bestResult}";

                    break;

                case GameModeType.Level:

                    _bestResultDisplayTextMesh.text = $"-";

                    break;
            }
        }
    }
}