using PauseManagment;
using UnityEngine;
using VContainer;

namespace Gameplay.UI
{
    public class PauseDisplay : MonoBehaviour
    {
        private PauseHandler _pauseHandler;
        private BlurBackground _blurBackground;

        [Inject]
        private void Construct(PauseHandler pauseHandler, BlurBackground blurBackground)
        {
            _pauseHandler = pauseHandler;
            _blurBackground = blurBackground;
        }

        private void OnEnable()
        {
            _pauseHandler.Paused += OnPaused;
            _pauseHandler.Unpaused += OnUnpaused;
        }

        private void OnDisable()
        {
            _pauseHandler.Paused -= OnPaused;
            _pauseHandler.Unpaused -= OnUnpaused;
        }

        private void OnPaused()
        {
            _blurBackground.Appear();
        }

        private void OnUnpaused()
        {
            _blurBackground.Disappear();
        }
    }
}