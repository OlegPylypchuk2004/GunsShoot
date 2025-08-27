using TMPro;
using UnityEngine;
using VContainer;

namespace ScoreSystem
{
    public class ScoreDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text _textMesh;

        private ScoreCounter _scoreCounter;

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
            UpdateDisplay(score);
        }

        private void UpdateDisplay(int score)
        {
            _textMesh.text = $"{score}";
        }
    }
}