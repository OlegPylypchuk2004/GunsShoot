using System;

namespace GameModeSystem
{
    public interface IGameMode
    {
        public bool IsCompleted { get; }

        public event Action<bool> GameOver;
    }
}