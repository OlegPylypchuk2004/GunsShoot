using DG.Tweening;
using UnityEngine;

namespace BlasterSystem
{
    public class AppearAnimator : BlasterAnimator
    {
        [SerializeField, Min(0f)] private float _duration;
        [SerializeField] private Ease _ease;
        [SerializeField] private Transform _meshTransform;

        protected override void Start()
        {
            base.Start();

            _meshTransform.DOScale(1f, _duration)
                .From(0f)
                .SetEase(_ease)
                .SetLink(gameObject);
        }
    }
}