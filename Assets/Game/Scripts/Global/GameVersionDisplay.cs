using TMPro;
using UnityEngine;

namespace Global
{
    public class GameVersionDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textMesh;

        private void OnEnable()
        {
            UpdateInfo();
        }

        public void UpdateInfo()
        {
            _textMesh.text = $"v{Application.version}";
        }
    }
}