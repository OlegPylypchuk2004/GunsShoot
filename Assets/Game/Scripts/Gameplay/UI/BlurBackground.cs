using DG.Tweening;
using LeTai.Asset.TranslucentImage;
using UnityEngine;

namespace Gameplay.UI
{
    public class BlurBackground : MonoBehaviour
    {
        [SerializeField] private TranslucentImage _translucentImage;
        [SerializeField] private BlurConfig _blurConfig;
        [SerializeField] private float _strength;
        [SerializeField] private float _duration;

        private Sequence _currentSequence;

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

            _currentSequence.Append(DOTween.To(() => _blurConfig.Strength, x => _blurConfig.Strength = x, _strength, _duration)
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

            _currentSequence.Append(DOTween.To(() => _blurConfig.Strength, x => _blurConfig.Strength = x, 0f, _duration)
                .SetEase(Ease.InQuad));

            return _currentSequence;
        }
    }
}