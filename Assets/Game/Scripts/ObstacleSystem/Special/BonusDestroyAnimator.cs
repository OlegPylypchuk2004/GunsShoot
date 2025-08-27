using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ObstacleSystem.Special
{
    public class BonusDestroyAnimator : MonoBehaviour
    {
        [SerializeField] private Bonus _bonus;
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private Image _iconImage;
        [SerializeField] private TMP_Text _textMesh;
        [SerializeField] private BonusDestroyAnimation _animationPrefab;

        private void OnEnable()
        {
            _bonus.Destroyed += OnBonusDestroyed;
        }

        private void OnDisable()
        {
            _bonus.Destroyed -= OnBonusDestroyed;
        }

        private void OnBonusDestroyed(Obstacle bonus)
        {
            _bonus.Destroyed -= OnBonusDestroyed;

            BonusDestroyAnimation animation = Instantiate(_animationPrefab);
            animation.transform.position = transform.position;
            animation.Initialize(_meshRenderer.material, _iconImage.sprite, _textMesh.text);
            animation.Play();
        }
    }
}