using UnityEngine;

namespace BlasterSystem
{
    public class BlasterAnimator : MonoBehaviour
    {
        [SerializeField] protected Blaster _blaster;
        [SerializeField] protected Transform _meshTransform;

        protected virtual void OnEnable()
        {
            _blaster.ShotFired += OnShotFired;
        }

        protected virtual void OnDisable()
        {
            _blaster.ShotFired -= OnShotFired;
        }

        protected virtual void OnShotFired()
        {

        }
    }
}