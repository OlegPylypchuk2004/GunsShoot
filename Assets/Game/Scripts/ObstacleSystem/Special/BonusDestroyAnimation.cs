using DG.Tweening;
using UnityEngine;

namespace ObstacleSystem.Special
{
    public class BonusDestroyAnimation : MonoBehaviour
    {
        [SerializeField] private Transform _meshTransform;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField, Min(0f)] private float _duration;
        [SerializeField] private float _canvasGroupYMoveDistance;
        [SerializeField] private Ease _meshEase;
        [SerializeField] private Ease _canvasGroupEase;

        private Sequence _sequence;

        public Sequence Play()
        {
            if (_sequence != null && _sequence.IsActive())
            {
                return null;
            }

            _sequence = DOTween.Sequence();
            _sequence.SetLink(gameObject);

            _sequence.Append(_meshTransform.DOScale(0f, _duration)
                .SetEase(_meshEase));

            _sequence.Join(_canvasGroup.GetComponent<RectTransform>().DOAnchorPosY(_canvasGroupYMoveDistance, _duration)
                .SetEase(_canvasGroupEase));

            _sequence.Join(_canvasGroup.DOFade(0f, _duration)
                .SetEase(_canvasGroupEase));

            _sequence.AppendCallback(() =>
            {
                Destroy(gameObject);
            });

            return _sequence;
        }
    }
}