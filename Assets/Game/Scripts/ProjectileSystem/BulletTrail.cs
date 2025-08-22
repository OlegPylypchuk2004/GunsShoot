using UnityEngine;

namespace ProjectileSystem
{
    public class BulletTrail : MonoBehaviour
    {
        [SerializeField] private TrailRenderer _trailRenderer;

        private void OnDisable()
        {
            _trailRenderer.Clear();
        }
    }
}