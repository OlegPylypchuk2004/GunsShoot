using BulletSystem;
using UnityEngine;

namespace BlasterSystem
{
    public class Blaster : MonoBehaviour
    {
        [SerializeField] private BlasterConfig _config;
        [SerializeField] private Transform _shootPoint;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            Bullet bullet = Instantiate(_config.Bullet.Prefab);
            bullet.transform.position = _shootPoint.position;
            bullet.Launch(_config.Bullet.Speed, -_shootPoint.right);
        }
    }
}