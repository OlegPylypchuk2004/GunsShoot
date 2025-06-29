using DG.Tweening;
using LeTai.Asset.TranslucentImage;
using UnityEngine;
using VContainer;

namespace UI
{
    public class BlurBackground : MonoBehaviour
    {
        [SerializeField] private TranslucentImage _translucentImage;
        [SerializeField] private float _maxAlpha;
        [SerializeField] private float _duration;

        private Sequence _currentSequence;

        [Inject]
        private void Construct(TranslucentImageSource translucentImageSource)
        {
            if (translucentImageSource == null)
            {
                _translucentImage.source = translucentImageSource;
            }
        }

        public Sequence Appear()
        {
            _currentSequence?.Kill();
            _currentSequence = DOTween.Sequence();
            _currentSequence.SetUpdate(true);
            _currentSequence.SetLink(gameObject);

            _currentSequence.AppendCallback(() =>
            {
                _translucentImage.gameObject.SetActive(true);
            });

            _currentSequence.Append(DOTween.To(() => _translucentImage.source.BlurConfig.Strength, x => _translucentImage.source.BlurConfig.Strength = x, _maxAlpha, _duration)
                .SetEase(Ease.OutQuad));

            return _currentSequence;
        }

        public Sequence Disappear()
        {
            _currentSequence?.Kill();
            _currentSequence = DOTween.Sequence();
            _currentSequence.SetUpdate(true);
            _currentSequence.SetLink(gameObject);

            _currentSequence.AppendCallback(() =>
            {
                _translucentImage.gameObject.SetActive(true);
            });

            _currentSequence.Append(DOTween.To(() => _translucentImage.source.BlurConfig.Strength, x => _translucentImage.source.BlurConfig.Strength = x, 0f, _duration)
                .SetEase(Ease.InQuad));

            return _currentSequence;
        }
    }
}