using SaveSystem;
using System;
using UnityEngine;

namespace BlasterSystem.Sounds
{
    public abstract class BlasterSoundPlayer : MonoBehaviour
    {
        [SerializeField] protected Blaster _blaster;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField, Range(0f, 1f)] private float _pitchSpread;

        private void Awake()
        {
            bool isSoundEnabled = SaveManager.Data.IsSoundEnabled;

            if (isSoundEnabled)
            {
                _audioSource.volume = 1f;
            }
            else
            {
                _audioSource.volume = 0f;
            }
        }

        protected virtual void OnEnable()
        {
            if (_blaster == null)
            {
                throw new Exception("Blaster is not set");
            }

            if (_audioSource == null)
            {
                throw new Exception("Audio source is not set");
            }
        }

        protected virtual void OnDisable()
        {

        }

        protected virtual void Play(AudioClip audioClip)
        {
            _audioSource.pitch = 1f + UnityEngine.Random.Range(-_pitchSpread, _pitchSpread);
            _audioSource.PlayOneShot(audioClip);
        }
    }
}