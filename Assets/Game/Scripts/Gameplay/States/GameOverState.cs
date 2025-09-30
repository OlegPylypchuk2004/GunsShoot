using CurrencyManagment;
using Cysharp.Threading.Tasks;
using Gameplay.UI;
using Patterns.StateMachine;
using RewardCountSystem;
using TimeManagment;

namespace Gameplay.States
{
    public class GameOverState : State
    {
        private GameOverPanel _gameOverDisplay;
        private TimeSlower _timeSlower;
        private RewardCounter _rewardCounter;
        private CurrencyWallet _currencyWallet;

        public GameOverState(GameOverPanel gameOverDisplay, TimeSlower timeSlower, RewardCounter rewardCounter, CurrencyWallet currencyWallet)
        {
            _gameOverDisplay = gameOverDisplay;
            _timeSlower = timeSlower;
            _rewardCounter = rewardCounter;
            _currencyWallet = currencyWallet;
        }

        public override void Enter()
        {
            base.Enter();

            _timeSlower.ResetTime();
            _currencyWallet.TryIncrease(_rewardCounter.CalculateReward());

            ShowGameOverDisplay().Forget();
        }

        private async UniTaskVoid ShowGameOverDisplay()
        {
            await UniTask.Delay(500);

            _gameOverDisplay.Appear();
        }
    }
}