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
            return _transform.DOShakePosition(_duration, _strength, _vibrations)
                .SetEase(_ease)
                .SetLink(gameObject);
        }
    }
}