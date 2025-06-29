using DG.Tweening;
using UnityEngine;

namespace BlasterSystem
{
    public class RecoilAnimator : BlasterAnimator
    {
        [SerializeField, Min(0f)] private float _force;
        [SerializeField, Min(0f)] private float _duration;
        [SerializeField] private Ease _ease;

        private Tween _currentTween;
        private Vector3 _initialPosition;

        private void Awake()
        {
            _initialPosition = transform.localPosition;
        }

        protected override void OnShotFired()
        {
            base.OnShotFired();

            _currentTween?.Kill();
            transform.localPosition = _initialPosition;

            _currentTween = _meshTransform.DOPunchPosition(Vector3.right * _force, _duration, 1)
                .SetEase(_ease)
                .SetLink(gameObject);
        }
    }
}