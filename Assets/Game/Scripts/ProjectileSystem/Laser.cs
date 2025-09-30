using DG.Tweening;
using UnityEngine;

namespace ProjectileSystem
{
    public class Laser : RaycastProjectile
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField, Range(0f, 1f)] private float _maxSpriteAlpha;
        [SerializeField] private Ease _disappearEase;

        private Tween _disappearTween;

        protected override void OnLaunched()
        {
            base.OnLaunched();

            Disappear();
        }

        private Tween Disappear()
        {
            _disappearTween?.Kill();

            _disappearTween = _spriteRenderer.DOFade(0f, _lifeTime)
                .From(_maxSpriteAlpha)
                .SetEase(_disappearEase)
                .SetLink(gameObject);

            return _disappearTween;
        }
    }
}