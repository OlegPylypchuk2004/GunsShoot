using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class StatDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _valueText;
        [SerializeField] private Image _fillImage;

        public void Initialize(string name)
        {
            _nameText.text = name;
        }

        public void SetValue(float value, float maxValue)
        {
            _valueText.text = $"{value}";
            _fillImage.fillAmount = value / maxValue;
        }
    }
}