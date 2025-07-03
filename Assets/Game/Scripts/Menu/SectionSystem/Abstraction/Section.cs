using DG.Tweening;
using UnityEngine;

namespace Menu.SectionSystem
{
    public abstract class Section : MonoBehaviour
    {
        [SerializeField] protected SectionChanger _sectionChanger;
        [SerializeField] protected CanvasGroup _canvasGroup;
        [SerializeField, Min(0f)] protected float _appearUIDuration;
        [SerializeField, Min(0f)] protected float _disappearUIDuration;
        [SerializeField] protected Ease _appearUIEase;
        [SerializeField] protected Ease _disappearUIEase;

        public virtual Sequence Appear()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.SetLink(gameObject);

            sequence.AppendCallback(() =>
            {
                gameObject.SetActive(true);
            });

            sequence.Append(_canvasGroup.DOFade(1f, _appearUIDuration)
                .From(0f)
                .SetEase(_appearUIEase));

            return sequence;
        }

        public virtual Sequence Disappear()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.SetLink(gameObject);

            sequence.Append(_canvasGroup.DOFade(0f, _disappearUIDuration)
                .SetEase(_disappearUIEase));

            sequence.OnComplete(() =>
            {
                gameObject.SetActive(false);
            });

            return sequence;
        }
    }
}