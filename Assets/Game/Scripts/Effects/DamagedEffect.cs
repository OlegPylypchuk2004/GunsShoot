using HealthSystem;
using VContainer;

namespace Effects
{
    public class DamagedEffect : VignetteEffect
    {
        private HealthManager _healthManager;

        [Inject]
        private void Construct(HealthManager healthManager)
        {
            _healthManager = healthManager;
        }

        private void OnEnable()
        {
            _healthManager.HealthReduced += OnHealthReduced;
        }

        private void OnDisable()
        {
            _healthManager.HealthReduced -= OnHealthReduced;
        }

        private void OnHealthReduced(int health)
        {
            Blink();
        }
    }
}