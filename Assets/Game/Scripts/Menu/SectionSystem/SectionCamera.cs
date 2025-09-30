using DG.Tweening;
using UnityEngine;

namespace Menu.SectionSystem
{
    public class SectionCamera : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float _moveDuration;
        [SerializeField] private Ease _moveEase;
        [SerializeField] private SectionChanger _sectionChanger;

        private Tween _currentTween;

        private void OnEnable()
        {
            _sectionChanger.Changed += OnSectionChanged;
        }

        private void OnDisable()
        {
            _sectionChanger.Changed -= OnSectionChanged;
        }

        private void OnSectionChanged(Section section)
        {
            _currentTween?.Kill();

            Vector3 targetPosition = section.transform.position;
            targetPosition.z = transform.position.z;

            _currentTween = transform.DOMove(targetPosition, _moveDuration)
                .SetEase(_moveEase)
                .SetLink(gameObject);
        }
    }
}