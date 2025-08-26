using GameModeSystem;
using Gameplay.UI;
using Patterns.StateMachine;

namespace Gameplay.States
{
    public class GameOverState : State
    {
        private IGameMode _gameMode;
        private GameOverPanel _gameOverDisplay;

        public GameOverState(IGameMode gameMode, GameOverPanel gameOverDisplay)
        {
            _gameMode = gameMode;
            _gameOverDisplay = gameOverDisplay;
        }

        public override void Enter()
        {
            base.Enter();

            _gameMode.SaveData();
            _gameOverDisplay.Appear();
        }
    }
}