using Cysharp.Threading.Tasks;
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

            ShowGameOverDisplay().Forget();
        }

        private async UniTaskVoid ShowGameOverDisplay()
        {
            await UniTask.Delay(500);

            _gameOverDisplay.Appear();
        }
    }
}