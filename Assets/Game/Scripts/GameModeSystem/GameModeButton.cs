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
        [SerializeField] private TextMeshProUGUI _nameTextMesh;

        public event Action<GameModeConfig> Selected;

        private void Awake()
        {
            _nameTextMesh.text = _gameModeConfig.DisplayName;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            Selected?.Invoke(_gameModeConfig);
        }
    }
}