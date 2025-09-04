using System;
using UnityEngine;

namespace BlasterSystem.Sounds
{
    public class ReloadSoundPlayer : BlasterSoundPlayer
    {
        [SerializeField] private AudioClip _reloadSound;

        protected override void OnEnable()
        {
            base.OnEnable();

            _blaster.StateChanged += OnBlasterStateChanged;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _blaster.StateChanged -= OnBlasterStateChanged;
        }

        private void OnBlasterStateChanged(BlasterState state)
        {
            if (state != BlasterState.Reloading)
            {
                return;
            }

            if (_reloadSound == null)
            {
                throw new Exception("Reload sound is not set");
            }

            Play(_reloadSound);
        }
    }
}