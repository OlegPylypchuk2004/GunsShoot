using DG.Tweening;
using UnityEngine;

namespace Menu.SectionSystem
{
    public abstract class Section : MonoBehaviour
    {
        [SerializeField] protected float _appearUIDuration; 
        [SerializeField] protected float _disappearUIDuration; 
        [SerializeField] protected Ease _appearUIEase; 
        [SerializeField] protected Ease _disappearUIEase; 

        public virtual Sequence Appear()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.SetLink(gameObject);

            return sequence;
        }

        public virtual Sequence Disappear()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.SetLink(gameObject);

            return sequence;
        }
    }
}