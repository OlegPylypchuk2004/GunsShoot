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
        [SerializeField] private Obstacle _obstacle;
        [SerializeField] private Image _currentFillImage;
        [SerializeField] private Image _resultFillImage;

        private Tween _resultFillImageTween;

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
            _currentFillImage.fillAmount = (float)health / _obstacle.MaxHealth;

            _resultFillImageTween?.Kill();

            _resultFillImageTween = _resultFillImage.DOFillAmount(_currentFillImage.fillAmount, _resultFillDuration)
                .SetDelay(_resultFillDelay)
                .SetEase(_resultFillEase)
                .SetLink(gameObject);
        }
    }
}