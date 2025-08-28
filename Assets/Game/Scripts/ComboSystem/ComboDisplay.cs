using DG.Tweening;
using ScoreSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace ComboSystem
{
    public class ComboDisplay : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TMP_Text _textMesh;
        [SerializeField] private Image _fillImage;
        [SerializeField] private float _animationDuration;
        [SerializeField] private float _maxCanvasGroupScale;
        [SerializeField] private Ease _appearEase;
        [SerializeField] private Ease _disappearEase;

        private ComboCounter _comboCounter;
        private ScoreCounter _scoreCounter;
        private Sequence _currentSequence;
        private bool _isActive;

        [Inject]
        private void Construct(ComboCounter comboCounter, ScoreCounter scoreCounter)
        {
            _comboCounter = comboCounter;
            _scoreCounter = scoreCounter;
        }

        private void Awake()
        {
            _canvasGroup.alpha = 0f;
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
            if (combo >= 1)
            {
                float scoreMultiplier = _scoreCounter.Multiplier;
                string formattedScoreMultiplier = scoreMultiplier.ToString(System.Globalization.CultureInfo.InvariantCulture);

                _textMesh.text = $"x{formattedScoreMultiplier}";
            }

            if (!_isActive)
            {
                _isActive = true;

                Appear();
            }
        }

        private void OnTimeChanged(float time)
        {
            _fillImage.fillAmount = time / 5f;

            if (time <= 0f)
            {
                if (_isActive)
                {
                    _isActive = false;

                    Disappear();
                }
            }
        }

        private Sequence Appear()
        {
            _currentSequence?.Kill();

            _currentSequence = DOTween.Sequence();
            _currentSequence.SetLink(gameObject);

            _currentSequence.Append(_canvasGroup.transform.DOScale(1f, _animationDuration)
                .From(_maxCanvasGroupScale)
                .SetEase(_disappearEase));

            _currentSequence.Join(_canvasGroup.DOFade(1f, _animationDuration)
                .From(0f)
                .SetEase(_appearEase));

            return _currentSequence;
        }

        private Sequence Disappear()
        {
            _currentSequence?.Kill();

            _currentSequence = DOTween.Sequence();
            _currentSequence.SetLink(gameObject);

            _currentSequence.Append(_canvasGroup.transform.DOScale(_maxCanvasGroupScale, _animationDuration)
                .From(1f)
                .SetEase(_disappearEase));

            _currentSequence.Join(_canvasGroup.DOFade(0f, _animationDuration)
                .From(1f)
                .SetEase(_disappearEase));

            return _currentSequence;
        }
    }
}