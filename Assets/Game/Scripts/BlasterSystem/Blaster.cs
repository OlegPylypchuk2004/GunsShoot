using BulletSystem;
using UnityEngine;

namespace BlasterSystem
{
    public class Blaster : MonoBehaviour
    {
        [SerializeField] private Bullet _bulletPrefab;
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
            Bullet bullet = Instantiate(_bulletPrefab);
            bullet.transform.position = _shootPoint.position;
            bullet.Launch(1f, -_shootPoint.right);
        }
    }
}