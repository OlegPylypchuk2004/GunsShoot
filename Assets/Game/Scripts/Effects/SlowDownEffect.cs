using DG.Tweening;
using TimeManagment;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Effects
{
    public class SlowDownEffect : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField, Range(0f, 1f)] private float _maxImageAlpha;
        [SerializeField] private float _animationDuration;
        [SerializeField] private Ease _appearEase;
        [SerializeField] private Ease _disappearEase;

        private TimeSlower _timeSlower;
        private Tween _tween;

        [Inject]
        private void Construct(TimeSlower timeSlower)
        {
            _timeSlower = timeSlower;
        }

        private void Awake()
        {
            Color imageTargetColor = _image.color;
            imageTargetColor.a = 0f;

            _image.color = imageTargetColor;
        }

        private void OnEnable()
        {
            _timeSlower.SlowDownStarted += OnSlownDownStarted;
            _timeSlower.SlowDownCompleted += OnSlownDownCompleted;
        }

        private void OnDisable()
        {
            _timeSlower.SlowDownStarted -= OnSlownDownStarted;
            _timeSlower.SlowDownCompleted -= OnSlownDownCompleted;
        }

        private void OnSlownDownStarted()
        {
            Appear();
        }

        private void OnSlownDownCompleted()
        {
            Disappear();
        }

        private Tween Appear()
        {
            _tween?.Kill();

            _image.gameObject.SetActive(true);

            _tween = _image.DOFade(_maxImageAlpha, _animationDuration)
                .SetEase(_appearEase)
                .SetUpdate(true)
                .SetLink(gameObject);

            return _tween;
        }

        private Tween Disappear()
        {
            _tween?.Kill();

            _image.gameObject.SetActive(true);

            _tween = _image.DOFade(0f, _animationDuration)
                .SetEase(_disappearEase)
                .SetUpdate(true)
                .SetLink(gameObject)
                .OnKill(() =>
                {
                    _image.gameObject.SetActive(false);
                });

            return _tween;
        }
    }
}