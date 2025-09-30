using DG.Tweening;
using UnityEngine;

namespace BlasterSystem
{
    public class ShootLightAnimator : BlasterAnimator
    {
        [SerializeField] private Light _light;
        [SerializeField, Min(0f)] private float _duration;
        [SerializeField] private Ease _ease;

        private Sequence _currentSequence;
        private float _maxLightIntensity;

        protected override void Awake()
        {
            base.Awake();

            _maxLightIntensity = _light.intensity;
        }

        protected override void OnShotFired()
        {
            base.OnShotFired();

            _currentSequence?.Kill();

            _currentSequence = DOTween.Sequence();
            _currentSequence.SetLink(gameObject);

            _currentSequence.AppendCallback(() =>
            {
                _light.enabled = true;
            });

            _currentSequence.Append(DOTween.To(() => _light.intensity, x => _light.intensity = x, 0f, _duration)
                .From(_maxLightIntensity)
                .SetEase(_ease));

            _currentSequence.AppendCallback(() =>
            {
                _light.enabled = false;
            });
        }
    }
}