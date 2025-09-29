using DG.Tweening;
using UnityEngine;
using VContainer;

namespace Gameplay.UI
{
    public class Panel : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField, Min(0f)] protected float _minUIScale;
        [SerializeField, Min(0f)] protected float _appearUIDuration;
        [SerializeField, Min(0f)] protected float _disappearUIDuration;
        [SerializeField] protected Ease _appearUIEase;
        [SerializeField] protected Ease _disappearUIEase;

        private BlurBackground _blurBackground;
        private Sequence _currentSequence;

        [Inject]
        private void Construct(BlurBackground blurBackground)
        {
            _blurBackground = blurBackground;
        }

        protected virtual void OnEnable()
        {

        }

        protected virtual void OnDisable()
        {

        }

        public virtual Sequence Appear()
        {
            _currentSequence?.Kill();

            _currentSequence = DOTween.Sequence();
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

            return _currentSequence;
        }

        public virtual Sequence Disappear()
        {
            _currentSequence?.Kill();

            _currentSequence = DOTween.Sequence();
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

            return _currentSequence;
        }
    }
}