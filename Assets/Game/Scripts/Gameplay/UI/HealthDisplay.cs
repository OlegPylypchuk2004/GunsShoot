using HealthSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Gameplay.UI
{
    public class HealthDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textMesh;
        [SerializeField] private Image _icon;

        private HealthManager _healthManager;

        [Inject]
        private void Construct(HealthManager healthManager)
        {
            _healthManager = healthManager;
        }

        private void OnEnable()
        {
            UpdateText(_healthManager.Health);

            _healthManager.HealthChanged += OnHealthChanged;
        }

        private void OnDisable()
        {
            _healthManager.HealthChanged -= OnHealthChanged;
        }

        private void LateUpdate()
        {
            transform.rotation = Quaternion.identity;
        }

        private void OnHealthChanged(int health)
        {
            UpdateText(health);
        }

        private void UpdateText(int health)
        {
            _textMesh.text = $"x{health}";
        }
    }
}