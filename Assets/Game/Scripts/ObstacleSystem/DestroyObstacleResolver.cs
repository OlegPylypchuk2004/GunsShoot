using BlasterSystem;
using HealthSystem;

namespace ObstacleSystem
{
    public class DestroyObstacleResolver
    {
        private BlasterHolder _blasterHolder;
        private HealthManager _healthManager;

        public DestroyObstacleResolver(BlasterHolder blasterHolder, HealthManager healthManager)
        {
            _blasterHolder = blasterHolder;
            _healthManager = healthManager;
        }

        public void Resolve(Obstacle obstacle)
        {
            switch (obstacle.Type)
            {
                case ObstacleType.Heart:

                    _healthManager.IncreaseHealth();

                    break;

                case ObstacleType.Blaster:

                    _blasterHolder.ChangeBlasterRandom();

                    break;
            }
        }
    }
}