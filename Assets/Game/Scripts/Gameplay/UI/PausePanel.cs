using DG.Tweening;
using PauseManagment;
using VContainer;

namespace Gameplay.UI
{
    public class PausePanel : Panel
    {
        private PauseHandler _pauseHandler;

        [Inject]
        private void Construct(PauseHandler pauseHandler)
        {
            _pauseHandler = pauseHandler;
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
            Appear()
                .SetUpdate(true);
        }

        private void OnUnpaused()
        {
            Disappear()
                .SetUpdate(true);
        }
    }
}