using CurrencyManagment;
using Global;
using ObstacleSystem;

namespace RewardCountSystem
{
    public class RewardCounter
    {
        private ObstacleContainer _obstacleContainer;
        private int _destroyedObstaclesCount;

        public RewardCounter(ObstacleContainer obstacleContainer)
        {
            _obstacleContainer = obstacleContainer;

            _obstacleContainer.ObstacleAdded += OnObstacleAdded;
            _obstacleContainer.ObstacleRemoved += OnObstacleRemoved;
        }

        ~RewardCounter()
        {
            _obstacleContainer.ObstacleAdded -= OnObstacleAdded;
            _obstacleContainer.ObstacleRemoved -= OnObstacleRemoved;
        }

        public WalletOperationData CalculateReward()
        {
            WalletOperationData creditsPerBoxReward = LocalGameData.GameModeConfig.CreditsPerBoxReward;

            return new WalletOperationData(creditsPerBoxReward.CurrencyConfig, creditsPerBoxReward.Count * _destroyedObstaclesCount);
        }

        private void OnObstacleAdded(Obstacle obstacle)
        {
            obstacle.Destroyed += OnObstacleDestroyed;
        }

        private void OnObstacleRemoved(Obstacle obstacle)
        {
            obstacle.Destroyed -= OnObstacleDestroyed;
        }

        private void OnObstacleDestroyed(Obstacle obstacle)
        {
            _destroyedObstaclesCount++;
        }
    }
}