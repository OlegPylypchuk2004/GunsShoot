using BlasterSystem;

namespace ObstacleSystem
{
    public class DestroyObstacleResolver
    {
        private BlasterHolder _blasterHolder;

        public DestroyObstacleResolver(BlasterHolder blasterHolder)
        {
            _blasterHolder = blasterHolder;
        }

        public void Resolve(Obstacle obstacle)
        {
            switch (obstacle.Type)
            {
                case ObstacleType.Blaster:

                    _blasterHolder.ChangeBlasterRandom();

                    break;
            }
        }
    }
}