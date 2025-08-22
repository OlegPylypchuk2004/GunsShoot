using System;
using System.Collections;
using UnityEngine;

namespace ProjectileSystem
{
    public class RaycastProjectile : Projectile
    {
        public event Action<RaycastProjectile> Expired;

        private Coroutine _lifeTimeCoroutine;

        public override void Launch(ProjectileData projectileData)
        {
            base.Launch(projectileData);

            if (_lifeTimeCoroutine != null)
            {
                StopCoroutine(_lifeTimeCoroutine);
                _lifeTimeCoroutine = null;
            }

            _lifeTimeCoroutine = StartCoroutine(CountLifeTime());
        }

        private IEnumerator CountLifeTime()
        {
            yield return new WaitForSeconds(0.5f);

            _lifeTimeCoroutine = null;

            Expired?.Invoke(this);
        }
    }
}