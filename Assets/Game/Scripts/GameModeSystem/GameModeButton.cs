using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameModeSystem
{
    public class GameModeButton : MonoBehaviour
    {
        [SerializeField] private GameModeConfig _gameModeConfig;
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _titleTextMesh;
        [SerializeField] private TextMeshProUGUI _subtitileTextMesh;
        [SerializeField] private TextMeshProUGUI _bottomTextMesh;

        public event Action<GameModeConfig> Selected;

        private void Awake()
        {
            UpdateDisplay();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
        }

        protected virtual void UpdateDisplay()
        {
            if (_gameModeConfig == null)
            {
                return;
            }

            _titleTextMesh.text = _gameModeConfig.DisplayName;
            _subtitileTextMesh.text = _gameModeConfig.DisplaySubtitle;
            _bottomTextMesh.text = string.Empty;
        }

        private void OnButtonClicked()
        {
            if (_gameModeConfig == null)
            {
                return;
            }

            Selected?.Invoke(_gameModeConfig);
        }
    }
}