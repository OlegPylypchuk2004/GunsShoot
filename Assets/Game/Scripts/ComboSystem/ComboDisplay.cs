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

        [Inject]
        private void Construct(ComboCounter comboCounter)
        {
            _comboCounter = comboCounter;
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
            _textMesh.text = $"{combo}";
        }

        private void OnTimeChanged(float time)
        {
            _fillImage.fillAmount = time / 5f;
        }
    }
}