using TMPro;
using UnityEngine;
using VContainer;

namespace StageSystem
{
    public class StagesLeftDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text _textMesh;

        private StageManager _stageManager;

        [Inject]
        private void Construct(StageManager stageManager)
        {
            _stageManager = stageManager;
        }

        private void OnEnable()
        {
            UpdateDisplay();

            _stageManager.StagesLoaded += OnStagesLoaded;
        }

        private void OnDisable()
        {
            _stageManager.StagesLoaded -= OnStagesLoaded;
            _stageManager.StageNumberChanged -= OnStageNumberChanged;
        }

        private void OnStagesLoaded()
        {
            _stageManager.StagesLoaded -= OnStagesLoaded;
            _stageManager.StageNumberChanged += OnStageNumberChanged;

            UpdateDisplay();
        }

        private void OnStageNumberChanged(int stageNumber)
        {
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            _textMesh.text = $"{_stageManager.StagesLeft}";
        }
    }
}