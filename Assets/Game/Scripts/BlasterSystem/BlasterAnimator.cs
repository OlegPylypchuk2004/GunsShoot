using UnityEngine;

namespace BlasterSystem
{
    public class BlasterAnimator : MonoBehaviour
    {
        [SerializeField] protected Blaster _blaster;

        protected virtual void Awake()
        {

        }

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