using DG.Tweening;
using PauseManagment;
using UnityEngine;
using VContainer;

namespace Gameplay.UI
{
    public class PauseDisplay : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        private PauseHandler _pauseHandler;
        private BlurBackground _blurBackground;

        private Sequence _currentSequence;

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
            _currentSequence?.Kill();

            _currentSequence = DOTween.Sequence();
            _currentSequence.SetUpdate(true);
            _currentSequence.SetLink(gameObject);

            _currentSequence.AppendCallback(() =>
            {
                _canvasGroup.interactable = false;
            });

            _currentSequence.Append(_blurBackground.Appear());

            _currentSequence.AppendCallback(() =>
            {
                _canvasGroup.gameObject.SetActive(true);
                _canvasGroup.interactable = true;
            });

            _currentSequence.Join(_canvasGroup.DOFade(1f, 0.25f)
                .From(0f)
                .SetEase(Ease.OutQuad));
        }

        private void OnUnpaused()
        {
            _currentSequence?.Kill();

            _currentSequence = DOTween.Sequence();
            _currentSequence.SetUpdate(true);
            _currentSequence.SetLink(gameObject);

            _currentSequence.AppendCallback(() =>
            {
                _canvasGroup.interactable = false;
            });

            _currentSequence.Append(_blurBackground.Disappear());

            _currentSequence.Join(_canvasGroup.DOFade(0f, 0.25f)
                .SetEase(Ease.InQuad));

            _currentSequence.AppendCallback(() =>
            {
                _canvasGroup.gameObject.SetActive(false);
            });
        }
    }
}