using System;
using System.Collections;
using UnityEngine;

namespace ObstacleSystem.Special
{
    public class Grenade : Obstacle
    {
        [SerializeField, Min(0f)] private float _explodeDelay;

        private Coroutine _explodeCoroutine;

        public event Action Exploded;

        public override void Launch(Vector3 direction, float gravityMultiplier)
        {
            base.Launch(direction, gravityMultiplier);

            if (_explodeCoroutine != null)
            {
                StopCoroutine(_explodeCoroutine);
                _explodeCoroutine = null;
            }

            _explodeCoroutine = StartCoroutine(Explode());
        }

        private IEnumerator Explode()
        {
            yield return new WaitForSeconds(_explodeDelay);

            TakeDamage(Health);

            Exploded?.Invoke();
        }
    }
}