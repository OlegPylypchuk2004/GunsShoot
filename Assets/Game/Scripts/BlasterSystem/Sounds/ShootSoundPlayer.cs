using System;
using UnityEngine;

namespace BlasterSystem.Sounds
{
    public class ShootSoundPlayer : BlasterSoundPlayer
    {
        [SerializeField] private AudioClip _shootSound;

        protected override void OnEnable()
        {
            base.OnEnable();

            _blaster.ShotFired += OnBlasterShotFired;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _blaster.ShotFired -= OnBlasterShotFired;
        }

        private void OnBlasterShotFired()
        {
            if (_shootSound == null)
            {
                throw new Exception("Shoot sound is not set");
            }

            Play(_shootSound);
        }
    }
}