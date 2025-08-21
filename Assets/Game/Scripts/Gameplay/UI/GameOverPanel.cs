using SceneManagment;
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

        private SceneLoader _sceneLoader;

        [Inject]
        private void Construct(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
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