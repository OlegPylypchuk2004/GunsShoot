using Gameplay.UI;
using Patterns.StateMachine;

namespace Gameplay.States
{
    public class GameOverState : State
    {
        private GameOverDisplay _gameOverDisplay;

        public GameOverState(GameOverDisplay gameOverDisplay)
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