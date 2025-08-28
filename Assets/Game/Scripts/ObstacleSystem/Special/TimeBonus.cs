using TimeManagment;
using VContainer;

namespace ObstacleSystem.Special
{
    public class TimeBonus : Bonus
    {
        private TimeScaler _timeScaler;

        [Inject]
        private void Construct(TimeScaler timeScaler)
        {
            _timeScaler = timeScaler;
        }

        protected override void Kill()
        {
            base.Kill();

            _timeScaler.SlowDown();
        }
    }
}