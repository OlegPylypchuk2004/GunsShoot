using DG.Tweening;
using TMPro;
using UnityEngine;

namespace DamageSystem
{
    public class DamageNumber : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float _appearDuration;
        [SerializeField, Min(0f)] private float _punchStrength;
        [SerializeField, Min(0f)] private float _lifeTime;
        [SerializeField] private Ease _punchEase;
        [SerializeField, Min(0f)] private float _disappearDuration;
        [SerializeField] private Ease _disappearEase;
        [SerializeField] private TextMeshPro _textMesh;

        public Sequence PlayAnimation(int damage, Color color)
        {
            _textMesh.text = $"{damage}";
            _textMesh.color = color;

            Sequence sequence = DOTween.Sequence();
            sequence.SetLink(gameObject);

            sequence.Append(transform.DOPunchScale(Vector3.one * _punchStrength, _appearDuration, 1)
                .SetEase(_punchEase));

            sequence.AppendInterval(_lifeTime);

            sequence.Append(transform.DOScale(0f, _disappearDuration)
                .SetEase(_disappearEase));

            sequence.AppendCallback(() =>
            {
                Destroy(gameObject);
            });

            return sequence;
        }
    }
}