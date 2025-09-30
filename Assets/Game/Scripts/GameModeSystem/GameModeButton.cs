using SaveSystem;
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
        [SerializeField] private TextMeshProUGUI _energyPriceTextMesh;

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

            SaveData saveData = SaveManager.Data;

            switch (_gameModeConfig.Type)
            {
                case GameModeType.Endless:

                    if (saveData.GameModes.ContainsKey(_gameModeConfig.ID))
                    {
                        _bottomTextMesh.text = $"{saveData.GameModes[_gameModeConfig.ID]}";
                    }
                    else
                    {
                        _bottomTextMesh.text = string.Empty;
                    }

                    break;

                case GameModeType.Level:

                    if (saveData.GameModes.ContainsKey(_gameModeConfig.ID))
                    {
                        _titleTextMesh.text += $" {saveData.GameModes[_gameModeConfig.ID]}";
                    }

                    _bottomTextMesh.text = string.Empty;

                    break;

                default:

                    if (saveData.GameModes.ContainsKey(_gameModeConfig.ID))
                    {
                        _bottomTextMesh.text = $"{saveData.GameModes[_gameModeConfig.ID]}";
                    }
                    else
                    {
                        _bottomTextMesh.text = string.Empty;
                    }

                    break;
            }

            _energyPriceTextMesh.text = $"{_gameModeConfig.EnergyPrice}";
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