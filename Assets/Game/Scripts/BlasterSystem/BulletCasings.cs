using UnityEngine;

namespace BlasterSystem
{
    public class BulletCasings : BlasterAnimator
    {
        [SerializeField] private ParticleSystem _particleSystem;

        protected override void OnShotFired()
        {
            base.OnShotFired();

            _particleSystem.Play();
        }
    }
}