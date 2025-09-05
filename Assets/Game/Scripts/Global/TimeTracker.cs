using SaveSystem;
using System;

namespace Global
{
    public class TimeTracker
    {
        public void SaveExitTime()
        {
            long currentTime = DateTime.UtcNow.ToBinary();
            SaveManager.Data.LastExitTime = currentTime.ToString();
            SaveManager.Save();
        }

        public TimeSpan GetTimeSinceLastExit()
        {
            string savedTime = SaveManager.Data.LastExitTime;

            if (long.TryParse(savedTime, out long binaryTime))
            {
                DateTime lastExitTime = DateTime.FromBinary(binaryTime);

                return DateTime.UtcNow - lastExitTime;
            }

            return TimeSpan.Zero;
        }
    }
}