using BlasterSystem;
using DG.Tweening;
using UnityEngine;

namespace CameraManagment
{
    public class CameraShaker : MonoBehaviour
    {
        [SerializeField] private Blaster _blaster;
        [SerializeField] private Transform _transform;
        [SerializeField] private float _duration;
        [SerializeField] private float _strength;
        [SerializeField] private int _vibrations;
        [SerializeField] private Ease _ease;

        private Vector3 _defaultPosition;
        private Tween _shakeTween;

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
            _blaster.ShotFired += OnShotFired;
        }

        private void OnDisable()
        {
            _blaster.ShotFired -= OnShotFired;
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