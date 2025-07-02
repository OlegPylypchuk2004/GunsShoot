using System;
using System.Collections.Generic;

namespace ObstacleSystem
{
    public class ObstacleContainer
    {
        private readonly List<Obstacle> _obstacles;

        public IReadOnlyList<Obstacle> Obstacles => _obstacles;

        public event Action<Obstacle> ObstacleAdded;
        public event Action<Obstacle> ObstacleRemoved;

        public ObstacleContainer()
        {
            _obstacles = new List<Obstacle>();
        }

        public bool TryAddObstacle(Obstacle obstacle)
        {
            if (obstacle == null)
            {
                return false;
            }

            if (_obstacles.Contains(obstacle))
            {
                return false;
            }

            _obstacles.Add(obstacle);

            ObstacleAdded?.Invoke(obstacle);

            return true;
        }

        public bool TryRemoveObstacle(Obstacle obstacle)
        {
            if (obstacle == null)
            {
                return false;
            }

            if (_obstacles.Remove(obstacle))
            {
                ObstacleRemoved?.Invoke(obstacle);

                return true;
            }

            return false;
        }
    }
}