using UnityEngine;
using VContainer;

namespace Global
{
    public class TimeTrackerSaver : MonoBehaviour
    {
        private TimeTracker _timeTracker;

        [Inject]
        private void Contruct(TimeTracker timeTracker)
        {
            _timeTracker = timeTracker;
        }

        private void Start()
        {
            Debug.Log($"Time since last exit: {_timeTracker.GetTimeSinceLastExit()}");
        }

        private void OnApplicationQuit()
        {
            _timeTracker.SaveExitTime();
        }
    }
}