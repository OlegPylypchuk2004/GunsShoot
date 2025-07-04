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

        public virtual void Activate()
        {
            gameObject.SetActive(true);
        }

        public virtual void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public virtual Sequence AppearUI()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.SetLink(gameObject);

            sequence.Append(_canvasGroup.DOFade(1f, _appearUIDuration)
                .From(0f)
                .SetEase(_appearUIEase));

            return sequence;
        }

        public virtual Sequence DisappearUI()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.SetLink(gameObject);

            sequence.Append(_canvasGroup.DOFade(0f, _disappearUIDuration)
                .SetEase(_disappearUIEase));

            return sequence;
        }
    }
}