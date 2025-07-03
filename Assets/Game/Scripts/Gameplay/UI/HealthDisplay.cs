using HealthSystem;
using System.Collections;
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
        [SerializeField] private Color _shineIconColor;
        [SerializeField, Min(0f)] private float _shineDuration;

        private HealthManager _healthManager;
        private Coroutine _iconAnimationCoroutine;
        private Color _defaultIconColor;

        [Inject]
        private void Construct(HealthManager healthManager)
        {
            _healthManager = healthManager;
        }

        private void Awake()
        {
            _defaultIconColor = _icon.color;
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

            if (_iconAnimationCoroutine != null)
            {
                StopCoroutine(_iconAnimationCoroutine);
                _iconAnimationCoroutine = null;
            }

            _iconAnimationCoroutine = StartCoroutine(PlayIconAnimationCoroutine());
        }

        private void UpdateText(int health)
        {
            _textMesh.text = $"x{health}";
        }

        private IEnumerator PlayIconAnimationCoroutine()
        {
            _icon.color = _shineIconColor;

            yield return new WaitForSeconds(_shineDuration);

            _icon.color = _defaultIconColor;
        }
    }
}