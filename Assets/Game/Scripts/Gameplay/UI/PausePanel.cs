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

        protected override void OnEnable()
        {
            base.OnEnable();

            _pauseHandler.Paused += OnPaused;
            _pauseHandler.Unpaused += OnUnpaused;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

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