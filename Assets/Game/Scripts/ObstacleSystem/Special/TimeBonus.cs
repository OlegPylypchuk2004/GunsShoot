using TimeManagment;
using UnityEngine;
using VContainer;

namespace ObstacleSystem.Special
{
    public class TimeBonus : Bonus
    {
        [SerializeField] private float _slowDownTime;

        private TimeSlower _timeScaler;

        [Inject]
        private void Construct(TimeSlower timeScaler)
        {
            _timeScaler = timeScaler;
        }

        protected override void Kill()
        {
            base.Kill();

            _timeScaler.SlowDown(_slowDownTime);
        }
    }
}