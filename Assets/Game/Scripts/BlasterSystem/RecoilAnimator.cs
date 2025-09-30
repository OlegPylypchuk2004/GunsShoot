using DG.Tweening;
using UnityEngine;

namespace BlasterSystem
{
    public class RecoilAnimator : BlasterAnimator
    {
        [SerializeField] private Transform _meshTransform;
        [SerializeField, Min(0f)] private float _punchPositionForce;
        [SerializeField, Min(0f)] private float _punchRotationForce;
        [SerializeField, Min(0f)] private float _duration;
        [SerializeField] private Ease _ease;

        private Sequence _currentSequence;
        private Vector3 _initialPosition;

        protected override void Awake()
        {
            base.Awake();

            _initialPosition = transform.localPosition;
        }

        protected override void OnShotFired()
        {
            base.OnShotFired();

            _currentSequence?.Kill();
            transform.localPosition = _initialPosition;

            _currentSequence = DOTween.Sequence();
            _currentSequence.SetLink(gameObject);

            _currentSequence.Join(_meshTransform.DOPunchPosition(Vector3.right * _punchPositionForce, _duration, 1)
                .SetEase(_ease));

            _currentSequence.Join(_meshTransform.DOPunchRotation(Vector3.right * _punchRotationForce * 100f, _duration, 1)
                .SetEase(_ease));
        }
    }
}