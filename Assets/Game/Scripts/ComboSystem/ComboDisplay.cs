using ScoreSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace ComboSystem
{
    public class ComboDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text _textMesh;
        [SerializeField] private Image _fillImage;

        private ComboCounter _comboCounter;
        private ScoreCounter _scoreCounter;

        [Inject]
        private void Construct(ComboCounter comboCounter, ScoreCounter scoreCounter)
        {
            _comboCounter = comboCounter;
            _scoreCounter = scoreCounter;
        }

        private void OnEnable()
        {
            _comboCounter.ComboChanged += OnComboChanged;
            _comboCounter.TimeChanged += OnTimeChanged;
        }

        private void OnDisable()
        {
            _comboCounter.ComboChanged -= OnComboChanged;
            _comboCounter.TimeChanged -= OnTimeChanged;
        }

        private void OnComboChanged(int combo)
        {
            float scoreMultiplier = _scoreCounter.Multiplier;
            string formattedScoreMultiplier = scoreMultiplier.ToString(System.Globalization.CultureInfo.InvariantCulture);

            _textMesh.text = $"x{formattedScoreMultiplier}";
        }

        private void OnTimeChanged(float time)
        {
            _fillImage.fillAmount = time / 5f;
        }
    }
}