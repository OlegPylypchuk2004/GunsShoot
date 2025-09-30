using DG.Tweening;
using UnityEngine;

namespace BlasterSystem
{
    public class BlasterReloadAnimator : BlasterAnimator
    {
        [SerializeField] private float _delay;
        [SerializeField] private Ease _ease;

        private Tween _currentTween;

        protected override void OnEnable()
        {
            base.OnEnable();

            _blaster.StateChanged += OnBlasterStateChanged;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _blaster.StateChanged -= OnBlasterStateChanged;
        }

        protected void OnBlasterStateChanged(BlasterState blasterState)
        {
            if (blasterState == BlasterState.Reloading)
            {
                PlayAnimation();
            }
        }

        private Tween PlayAnimation()
        {
            _currentTween?.Kill();

            _currentTween = transform.DOLocalRotate(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, -360f), _blaster.Config.ReloadDuration - _delay, RotateMode.FastBeyond360)
                .SetDelay(_delay)
                .SetEase(_ease)
                .SetLink(gameObject);

            return _currentTween;
        }
    }
}