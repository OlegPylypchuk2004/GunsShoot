using System;
using UnityEngine;

namespace PauseManagment
{
    public class PauseHandler
    {
        private bool _isPaused;
        private float _cashedTimeScale;

        public event Action Paused;
        public event Action Unpaused;

        public bool IsPaused
        {
            get => _isPaused;
            set
            {
                if (value == _isPaused)
                {
                    return;
                }

                _isPaused = value;

                if (_isPaused)
                {
                    _cashedTimeScale = Time.timeScale;
                    Time.timeScale = 0f;

                    Paused?.Invoke();
                }
                else
                {
                    Time.timeScale = _cashedTimeScale;

                    Unpaused?.Invoke();
                }
            }
        }
    }
}