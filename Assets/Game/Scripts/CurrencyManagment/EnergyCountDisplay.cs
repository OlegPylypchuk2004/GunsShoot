using EnergySystem;
using TMPro;
using UnityEngine;
using VContainer;

namespace CurrencyManagment
{
    public class EnergyCountDisplay : CurrencyCountDisplay
    {
        [SerializeField] private TMP_Text _timeTextMesh;

        private EnergyManager _energyManager;

        [Inject]
        private void Construct(EnergyManager energyManager)
        {
            _energyManager = energyManager;
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            _energyManager.RecoveryTimeChanged += OnRecoveryTimeChanged;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _energyManager.RecoveryTimeChanged -= OnRecoveryTimeChanged;
        }

        private void OnRecoveryTimeChanged(float time)
        {
            int minutes = Mathf.FloorToInt(time / 60f);
            int seconds = Mathf.FloorToInt(time % 60f);

            _timeTextMesh.text = $"{minutes:00}:{seconds:00}";
        }
    }
}