using BlasterSystem;
using DG.Tweening;
using UnityEngine;
using VContainer;

namespace CameraManagment
{
    public class CameraShaker : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private float _duration;
        [SerializeField] private float _strength;
        [SerializeField] private int _vibrations;
        [SerializeField] private Ease _ease;

        private BlasterHolder _blasterHolder;
        private Vector3 _defaultPosition;
        private Tween _shakeTween;

        [Inject]
        private void Construct(BlasterHolder blasterHolder)
        {
            _blasterHolder = blasterHolder;
        }

        private void Awake()
        {
            _defaultPosition = transform.localPosition;
        }

        private void OnValidate()
        {
            _transform ??= transform;
        }

        private void OnEnable()
        {
            _blasterHolder.Blaster.ShotFired += OnShotFired;
        }

        private void OnDisable()
        {
            _blasterHolder.Blaster.ShotFired -= OnShotFired;
        }

        private void OnShotFired()
        {
            Shake();
        }

        public Tween Shake()
        {
            _shakeTween?.Kill();

            _shakeTween = _transform.DOShakePosition(_duration, _strength, _vibrations)
                .SetEase(_ease)
                .SetLink(gameObject)
                .OnKill(() =>
                {
                    transform.localPosition = _defaultPosition;
                });

            return _shakeTween;
        }
    }
}