using System;
using UnityEngine;

namespace PauseManagment
{
    public class PauseHandler
    {
        private bool _isPaused;

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
                    Time.timeScale = 0f;

                    Paused?.Invoke();
                }
                else
                {
                    Time.timeScale = 1f;

                    Unpaused?.Invoke();
                }
            }
        }

        public event Action Paused;
        public event Action Unpaused;

        public void Update()
        {
            if (Input.GetMouseButton(1))
            {
                IsPaused = false;
            }
            else
            {
                IsPaused = true;
            }
        }
    }
}