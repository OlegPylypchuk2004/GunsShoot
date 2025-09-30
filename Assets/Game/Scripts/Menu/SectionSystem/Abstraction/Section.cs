using DG.Tweening;
using UnityEngine;

namespace Menu.SectionSystem
{
    public abstract class Section : MonoBehaviour
    {
        [SerializeField] protected SectionChanger _sectionChanger;
        [SerializeField] protected CanvasGroup _canvasGroup;
        [SerializeField, Min(0f)] protected float _minUIScale;
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

            sequence.AppendCallback(() =>
            {
                _canvasGroup.interactable = false;
            });

            sequence.Append(_canvasGroup.DOFade(1f, _appearUIDuration)
                .From(0f)
                .SetEase(_appearUIEase));

            sequence.Join(_canvasGroup.transform.DOScale(1f, _appearUIDuration)
                .From(_minUIScale)
                .SetEase(_appearUIEase));

            sequence.OnComplete(() =>
            {
                _canvasGroup.interactable = true;

                OnUIAppeared();
            });

            return sequence;
        }

        public virtual Sequence DisappearUI()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.SetLink(gameObject);

            sequence.AppendCallback(() =>
            {
                _canvasGroup.interactable = false;
            });

            sequence.Append(_canvasGroup.DOFade(0f, _disappearUIDuration)
                .SetEase(_disappearUIEase));

            sequence.Join(_canvasGroup.transform.DOScale(_minUIScale, _disappearUIDuration)
                .From(1f)
                .SetEase(_disappearUIEase));

            sequence.OnComplete(() =>
            {
                _canvasGroup.interactable = true;

                OnUIDisappeared();
            });

            return sequence;
        }

        protected virtual void OnUIAppeared()
        {

        }

        protected virtual void OnUIDisappeared()
        {

        }
    }
}