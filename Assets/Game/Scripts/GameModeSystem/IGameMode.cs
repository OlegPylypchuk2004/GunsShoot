using System;

namespace GameModeSystem
{
    public interface IGameMode
    {
        public event Action<bool> GameOver;

        public void SaveData();
    }
}