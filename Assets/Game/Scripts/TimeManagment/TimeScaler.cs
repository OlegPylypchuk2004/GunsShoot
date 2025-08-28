using UnityEngine;

namespace TimeManagment
{
    public class TimeScaler
    {
        private TimeConfig _timeConfig;

        public TimeScaler(TimeConfig timeConfig)
        {
            _timeConfig = timeConfig;
        }

        public void SlowDown()
        {
            Time.timeScale = _timeConfig.SlowDownValue;
        }
    }
}