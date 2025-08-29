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

        private Tween _tween;

        private void Awake()
        {
            Color imageTargetColor = _image.color;
            imageTargetColor.a = 0f;

            _image.color = imageTargetColor;
        }

        protected Tween Appear()
        {
            _tween?.Kill();

            _image.gameObject.SetActive(true);

            _tween = _image.DOFade(_maxImageAlpha, _animationDuration)
                .SetEase(_appearEase)
                .SetUpdate(true)
                .SetLink(gameObject);

            return _tween;
        }

        protected Tween Disappear()
        {
            _tween?.Kill();

            _image.gameObject.SetActive(true);

            _tween = _image.DOFade(0f, _animationDuration)
                .SetEase(_disappearEase)
                .SetUpdate(true)
                .SetLink(gameObject)
                .OnKill(() =>
                {
                    _image.gameObject.SetActive(false);
                });

            return _tween;
        }
    }
}