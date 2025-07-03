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
        private Blaster _previousBlaster;
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
            _blasterHolder.BlasterChanged += OnBlasterChanged;

            if (_blasterHolder.Blaster != null)
            {
                _previousBlaster = _blasterHolder.Blaster;
                _previousBlaster.ShotFired += OnShotFired;
            }
        }

        private void OnDisable()
        {
            _blasterHolder.BlasterChanged -= OnBlasterChanged;

            if (_previousBlaster != null)
            {
                _previousBlaster.ShotFired -= OnShotFired;
                _previousBlaster = null;
            }
        }

        private void OnBlasterChanged(Blaster blaster)
        {
            if (_previousBlaster != null)
            {
                _previousBlaster.ShotFired -= OnShotFired;
            }

            if (blaster != null)
            {
                blaster.ShotFired += OnShotFired;
                _previousBlaster = blaster;
            }
            else
            {
                _previousBlaster = null;
            }
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