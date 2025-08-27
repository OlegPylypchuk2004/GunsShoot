using UnityEngine;

namespace ObstacleSystem.Special
{
    public class BonusDestroyAnimator : MonoBehaviour
    {
        [SerializeField] private Bonus _bonus;
        [SerializeField] private BonusDestroyAnimation _animationPrefab;

        private void OnEnable()
        {
            _bonus.Destroyed += OnBonusDestroyed;
        }

        private void OnDisable()
        {
            _bonus.Destroyed -= OnBonusDestroyed;
        }

        private void OnBonusDestroyed(Obstacle bonus)
        {
            _bonus.Destroyed -= OnBonusDestroyed;

            BonusDestroyAnimation animation = Instantiate(_animationPrefab);
            animation.transform.position = transform.position;
            animation.Play();
        }
    }
}