using DG.Tweening;
using PauseManagment;
using UnityEngine;
using VContainer;

namespace Gameplay.UI
{
    public class PauseDisplay : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField, Min(0f)] protected float _minUIScale;
        [SerializeField, Min(0f)] protected float _appearUIDuration;
        [SerializeField, Min(0f)] protected float _disappearUIDuration;
        [SerializeField] protected Ease _appearUIEase;
        [SerializeField] protected Ease _disappearUIEase;

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
                _canvasGroup.gameObject.SetActive(true);
                _canvasGroup.interactable = false;
            });

            _currentSequence.Append(_blurBackground.Appear());

            _currentSequence.Join(_canvasGroup.DOFade(1f, _appearUIDuration)
                .From(0f)
                .SetEase(Ease.OutQuad));

            _currentSequence.Join(_canvasGroup.transform.DOScale(1f, _appearUIDuration)
                .From(_minUIScale)
                .SetEase(_appearUIEase));

            _currentSequence.AppendCallback(() =>
            {
                _canvasGroup.interactable = true;
            });
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

            _currentSequence.Append(_canvasGroup.DOFade(0f, _disappearUIDuration)
                .SetEase(Ease.InQuad));

            _currentSequence.Join(_canvasGroup.transform.DOScale(_minUIScale, _disappearUIDuration)
                .SetEase(_disappearUIEase));

            _currentSequence.Join(_blurBackground.Disappear());

            _currentSequence.AppendCallback(() =>
            {
                _canvasGroup.gameObject.SetActive(false);
            });
        }
    }
}