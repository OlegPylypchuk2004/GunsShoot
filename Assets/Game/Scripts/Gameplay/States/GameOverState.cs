using Cysharp.Threading.Tasks;
using Gameplay.UI;
using Patterns.StateMachine;
using TimeManagment;

namespace Gameplay.States
{
    public class GameOverState : State
    {
        private GameOverPanel _gameOverDisplay;
        private TimeSlower _timeSlower;

        public GameOverState(GameOverPanel gameOverDisplay, TimeSlower timeSlower)
        {
            _gameOverDisplay = gameOverDisplay;
            _timeSlower = timeSlower;
        }

        public override void Enter()
        {
            base.Enter();

            _timeSlower.ResetTime();

            ShowGameOverDisplay().Forget();
        }

        private async UniTaskVoid ShowGameOverDisplay()
        {
            await UniTask.Delay(500);

            _gameOverDisplay.Appear();
        }
    }
}