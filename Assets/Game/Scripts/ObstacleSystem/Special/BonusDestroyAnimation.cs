using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ObstacleSystem.Special
{
    public class BonusDestroyAnimation : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private Image _iconImage;
        [SerializeField] private TMP_Text _textMesh;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField, Min(0f)] private float _duration;
        [SerializeField] private float _canvasGroupYMoveDistance;
        [SerializeField] private Ease _meshEase;
        [SerializeField] private Ease _canvasGroupEase;

        private Sequence _sequence;

        public void Initialize(Material material, Sprite iconSprite, string text)
        {
            _meshRenderer.material = material;
            _iconImage.sprite = iconSprite;
            _textMesh.text = text;
        }

        public Sequence Play()
        {
            if (_sequence != null && _sequence.IsActive())
            {
                return null;
            }

            _sequence = DOTween.Sequence();
            _sequence.SetLink(gameObject);

            _sequence.Append(_meshRenderer.transform.DOScale(0f, _duration)
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