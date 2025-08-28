using HealthSystem;
using VContainer;

namespace ObstacleSystem.Special
{
    public class HeartBonus : Bonus
    {
        private HealthManager _healthManager;

        [Inject]
        private void Construct(HealthManager healthManager)
        {
            _healthManager = healthManager;
        }

        protected override void Kill()
        {
            base.Kill();

            _healthManager.IncreaseHealth();
        }
    }
}