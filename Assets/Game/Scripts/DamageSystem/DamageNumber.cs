using DG.Tweening;
using TMPro;
using UnityEngine;

namespace DamageSystem
{
    public class DamageNumber : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float _duration;
        [SerializeField, Min(0f)] private float _strength;
        [SerializeField, Min(0f)] private float _lifeTime;
        [SerializeField] private Ease _ease;
        [SerializeField] private TextMeshPro _textMesh;

        public Sequence PlayAnimation(int damage, Color color)
        {
            _textMesh.text = $"{damage}";
            _textMesh.color = color;

            Sequence sequence = DOTween.Sequence();
            sequence.SetLink(gameObject);

            sequence.Append(transform.DOPunchScale(Vector3.one * _strength, _duration, 1)
                .SetEase(_ease));

            sequence.AppendInterval(_lifeTime);

            sequence.AppendCallback(() =>
            {
                Destroy(gameObject);
            });

            return sequence;
        }
    }
}