using UnityEngine;

namespace TimeManagment
{
    public class TimeSlower
    {
        private TimeConfig _timeConfig;
        private bool _isSlowDown;
        private float _time;

        public TimeSlower(TimeConfig timeConfig)
        {
            _timeConfig = timeConfig;
        }

        public void Update()
        {
            if (_isSlowDown)
            {
                if (_time > 0f)
                {
                    _time -= Time.unscaledDeltaTime;

                    if (_time <= 0f)
                    {
                        _isSlowDown = false;
                        _time = 0f;

                        Time.timeScale = 1f;
                    }
                }
            }
        }

        public void SlowDown(float time)
        {
            _isSlowDown = true;
            _time = time;

            Time.timeScale = _timeConfig.SlowDownValue;
        }
    }
}