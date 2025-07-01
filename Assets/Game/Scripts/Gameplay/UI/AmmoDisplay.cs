using BlasterSystem;
using BlasterSystem.Abstractions;
using UnityEngine;

namespace Gameplay.UI
{
    public class AmmoDisplay : MonoBehaviour
    {
        [SerializeField] private GameObject[] _icons;
        
        //private IBlasterAmmoAmountReadonly _blasterAmmoAmountReadonly;
        public Blaster _blasterAmmoAmountReadonly;

        private void Awake()
        {
            //_blasterAmmoAmountReadonly = GetComponentInParent<IBlasterAmmoAmountReadonly>();
        }

        private void OnEnable()
        {
            if (_blasterAmmoAmountReadonly == null)
            {
                Debug.Log("Blaster not found");
            }
            else
            {
                _blasterAmmoAmountReadonly.AmmoAmountChanged += AmmoAmountChanged;
            }
        }

        private void OnDisable()
        {
            if (_blasterAmmoAmountReadonly == null)
            {
                _blasterAmmoAmountReadonly.AmmoAmountChanged -= AmmoAmountChanged;
            }
        }

        private void LateUpdate()
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }

        private void AmmoAmountChanged(int ammoAmount)
        {
            for (int i = 0; i < _icons.Length; i++)
            {
                _icons[i].SetActive(i < ammoAmount);
            }
        }
    }
}