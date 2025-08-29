using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Effects
{
    public class VignetteEffect : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField, Range(0f, 1f)] private float _maxImageAlpha;
        [SerializeField] private float _animationDuration;
        [SerializeField] private Ease _appearEase;
        [SerializeField] private Ease _disappearEase;

        private Tween _currentTween;
        private Sequence _currentSequence;

        private void Awake()
        {
            Color imageTargetColor = _image.color;
            imageTargetColor.a = 0f;

            _image.color = imageTargetColor;
        }

        protected Sequence Blink()
        {
            _currentTween?.Kill();
            _currentSequence?.Kill();

            _currentSequence = DOTween.Sequence();
            _currentSequence.SetLink(gameObject);

            _currentSequence.Append(Appear());
            _currentSequence.Append(Disappear());

            return _currentSequence;
        }

        protected Tween Appear()
        {
            _currentTween?.Kill();

            _image.gameObject.SetActive(true);

            _currentTween = _image.DOFade(_maxImageAlpha, _animationDuration)
                .SetEase(_appearEase)
                .SetUpdate(true)
                .SetLink(gameObject);

            return _currentTween;
        }

        protected Tween Disappear()
        {
            _currentTween?.Kill();

            _image.gameObject.SetActive(true);

            _currentTween = _image.DOFade(0f, _animationDuration)
                .SetEase(_disappearEase)
                .SetUpdate(true)
                .SetLink(gameObject)
                .OnKill(() =>
                {
                    _image.gameObject.SetActive(false);
                });

            return _currentTween;
        }
    }
}