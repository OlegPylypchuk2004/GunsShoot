using DG.Tweening;
using TMPro;
using UnityEngine;
using VContainer;

namespace ScoreSystem
{
    public class ScoreDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text _textMesh;
        [SerializeField, Min(0f)] private float _animationDuration;
        [SerializeField] private Ease _animationEase;

        private ScoreCounter _scoreCounter;
        private Tween _currentTween;
        private int _currentDisplayedCount;

        [Inject]
        private void Construct(ScoreCounter scoreCounter)
        {
            _scoreCounter = scoreCounter;
        }

        private void OnEnable()
        {
            UpdateDisplay(_scoreCounter.Score);

            _scoreCounter.ScoreChanged += OnScoreChanged;
        }

        private void OnDisable()
        {
            _scoreCounter.ScoreChanged -= OnScoreChanged;
        }

        private void OnScoreChanged(int score)
        {
            _currentTween?.Kill();

            _currentTween = DOTween.To(() => _currentDisplayedCount, value => { _currentDisplayedCount = value; UpdateDisplay(value); }, _scoreCounter.Score, _animationDuration)
                .SetEase(_animationEase)
                .SetLink(gameObject);
        }

        private void UpdateDisplay(int score)
        {
            _textMesh.text = $"{score}";
        }
    }
}