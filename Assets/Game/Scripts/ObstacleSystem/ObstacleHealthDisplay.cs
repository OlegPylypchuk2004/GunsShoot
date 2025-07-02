using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ObstacleSystem
{
    public class ObstacleHealthDisplay : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float _resultFillDuration;
        [SerializeField, Min(0f)] private float _resultFillDelay;
        [SerializeField] private Ease _resultFillEase;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Image _currentFillImage;
        [SerializeField] private Image _resultFillImage;

        private Obstacle _obstacle;
        private Tween _resultFillImageTween;

        private void Awake()
        {
            _canvas.worldCamera = Camera.main;
            _obstacle = GetComponentInParent<Obstacle>();

            _currentFillImage.fillAmount = 1f;
            _resultFillImage.fillAmount = 1f;
        }

        private void OnEnable()
        {
            _obstacle.HealthChanged += OnHealthChanged;
        }

        private void OnDisable()
        {
            _obstacle.HealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged(int health)
        {
            float value = (float)health / _obstacle.MaxHealth;

            _currentFillImage.fillAmount = value;

            _resultFillImageTween?.Kill();
            _resultFillImageTween = _resultFillImage.DOFillAmount(value, _resultFillDuration)
                .SetDelay(_resultFillDelay)
                .SetEase(_resultFillEase)
                .SetLink(gameObject);
        }
    }
}