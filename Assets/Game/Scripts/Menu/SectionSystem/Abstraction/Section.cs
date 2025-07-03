using DG.Tweening;
using UnityEngine;

namespace Menu.SectionSystem
{
    public abstract class Section : MonoBehaviour
    {
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

            return sequence;
        }

        public virtual Sequence Disappear()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.SetLink(gameObject);

            sequence.OnComplete(() =>
            {
                gameObject.SetActive(false);
            });

            return sequence;
        }
    }
}