using System.Globalization;
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

        public void SetValue(StatData statData, float value)
        {
            _nameText.text = statData.Name;
            _valueText.text = value.ToString(CultureInfo.InvariantCulture);
            _fillImage.fillAmount = value / statData.MaxValue;
        }
    }
}