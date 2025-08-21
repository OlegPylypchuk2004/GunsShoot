using Gameplay.UI;
using Patterns.StateMachine;

namespace Gameplay.States
{
    public class GameOverState : State
    {
        private GameOverPanel _gameOverDisplay;

        public GameOverState(GameOverPanel gameOverDisplay)
        {
            _gameOverDisplay = gameOverDisplay;
        }

        public override void Enter()
        {
            base.Enter();

            _gameOverDisplay.Appear();
        }
    }
}