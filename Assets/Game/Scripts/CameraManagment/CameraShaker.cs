using BlasterSystem;
using DG.Tweening;
using ObstacleSystem;
using UnityEngine;
using VContainer;

namespace CameraManagment
{
    public class CameraShaker : MonoBehaviour
    {
        [SerializeField] private ShakeData _shootShakeData;
        [SerializeField] private ShakeData _obstacleDestroyShakeData;
        [SerializeField] private Transform _transform;

        private Vector3 _defaultPosition;
        private Tween _shakeTween;
        private BlasterHolder _blasterHolder;
        private Blaster _previousBlaster;
        private ObstacleContainer _obstacleContainer;

        [Inject]
        private void Construct(BlasterHolder blasterHolder, ObstacleContainer obstacleContainer)
        {
            _blasterHolder = blasterHolder;
            _obstacleContainer = obstacleContainer;
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

            _obstacleContainer.ObstacleAdded += OnObstacleAdded;
            _obstacleContainer.ObstacleRemoved += OnObstacleRemoved;
        }

        private void OnDisable()
        {
            _blasterHolder.BlasterChanged -= OnBlasterChanged;

            if (_previousBlaster != null)
            {
                _previousBlaster.ShotFired -= OnShotFired;
                _previousBlaster = null;
            }

            _obstacleContainer.ObstacleAdded -= OnObstacleAdded;
            _obstacleContainer.ObstacleRemoved -= OnObstacleRemoved;
        }

        public Tween Shake(ShakeData shakeData)
        {
            _shakeTween?.Kill();

            _shakeTween = _transform.DOShakePosition(shakeData.Duration, shakeData.Strength, shakeData.Vibrations)
                .SetEase(shakeData.Ease)
                .SetLink(gameObject)
                .OnKill(() =>
                {
                    transform.localPosition = _defaultPosition;
                });

            return _shakeTween;
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
            Shake(_shootShakeData);
        }

        private void OnObstacleAdded(Obstacle obstacle)
        {
            obstacle.Destroyed += OnObstacleDestroyed;
        }

        private void OnObstacleRemoved(Obstacle obstacle)
        {
            obstacle.Destroyed -= OnObstacleDestroyed;
        }

        private void OnObstacleDestroyed(Obstacle obstacle)
        {
            Shake(_obstacleDestroyShakeData);
        }
    }
}