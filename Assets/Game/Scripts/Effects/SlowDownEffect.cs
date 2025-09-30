using TimeManagment;
using VContainer;

namespace Effects
{
    public class SlowDownEffect : VignetteEffect
    {
        private TimeSlower _timeSlower;

        [Inject]
        private void Construct(TimeSlower timeSlower)
        {
            _timeSlower = timeSlower;
        }

        private void OnEnable()
        {
            _timeSlower.SlowDownStarted += OnSlownDownStarted;
            _timeSlower.SlowDownCompleted += OnSlownDownCompleted;
        }

        private void OnDisable()
        {
            _timeSlower.SlowDownStarted -= OnSlownDownStarted;
            _timeSlower.SlowDownCompleted -= OnSlownDownCompleted;
        }

        private void OnSlownDownStarted()
        {
            Appear();
        }

        private void OnSlownDownCompleted()
        {
            Disappear();
        }
    }
}