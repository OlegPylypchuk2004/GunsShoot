using BlasterSystem.Abstractions;
using System.Collections;
using UnityEngine;

namespace BlasterSystem.Effects.MuzzleEffect
{
    public class MuzzleEffect : MonoBehaviour
    {
        [SerializeField] private Sprite[] _sprites;
        [SerializeField, Min(0f)] private float _changeSpriteDelay;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private IBlasterShotReadonly _blasterShotReadonly;
        private Coroutine _playAnimationCoroutine;

        private void Awake()
        {
            _blasterShotReadonly = GetComponentInParent<IBlasterShotReadonly>();
            _spriteRenderer.sprite = null;
        }

        private void OnEnable()
        {
            if (_blasterShotReadonly == null)
            {
                Debug.Log("Blaster not found");
            }
            else
            {
                _blasterShotReadonly.ShotFired += OnShotFired;
            }
        }

        private void OnDisable()
        {
            if (_blasterShotReadonly == null)
            {
                _blasterShotReadonly.ShotFired -= OnShotFired;
            }
        }

        private void OnShotFired()
        {
            if (_playAnimationCoroutine != null)
            {
                StopCoroutine(_playAnimationCoroutine);
            }

            _playAnimationCoroutine = StartCoroutine(PlayAnimation());
        }

        private IEnumerator PlayAnimation()
        {
            int spriteIndex = 0;

            while (spriteIndex < _sprites.Length - 1)
            {
                _spriteRenderer.sprite = _sprites[spriteIndex];

                yield return new WaitForSeconds(_changeSpriteDelay);

                spriteIndex++;
            }

            _spriteRenderer.sprite = null;
        }
    }
}