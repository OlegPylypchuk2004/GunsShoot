using UnityEngine;

namespace ProjectileSystem
{
    public class Laser : RaycastProjectile
    {
        [SerializeField] private float _distance;

        public override void Launch(ProjectileData projectileData)
        {
            base.Launch(projectileData);

            UpdateRotation();
        }

        private void UpdateRotation()
        {
            float angle = Mathf.Atan2(_projectileData.Direction.y, _projectileData.Direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}