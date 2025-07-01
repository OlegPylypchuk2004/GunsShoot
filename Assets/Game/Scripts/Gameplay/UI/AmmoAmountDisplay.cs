using BlasterSystem;
using System;
using UnityEngine;

namespace Gameplay.UI
{
    public class AmmoAmountDisplay : MonoBehaviour
    {
        [SerializeField] private AmmoDisplay _ammoDisplayPrefab;
        [SerializeField] private RectTransform _ammoDisplayParent;

        //private IBlasterAmmoAmountReadonly _blasterAmmoAmountReadonly;
        public Blaster _blasterAmmoAmountReadonly;
        private AmmoDisplay[] _ammoDisplays;

        private void Awake()
        {
            //_blasterAmmoAmountReadonly = GetComponentInParent<IBlasterAmmoAmountReadonly>();

            if (_blasterAmmoAmountReadonly == null)
            {
                throw new Exception("Blaster not found");
            }
        }

        private void Start()
        {
            if (_blasterAmmoAmountReadonly == null)
            {
                throw new Exception("Blaster not found");
            }

            _ammoDisplays = new AmmoDisplay[_blasterAmmoAmountReadonly.MaxAmmoAmount];

            for (int i = 0; i < _ammoDisplays.Length; i++)
            {
                _ammoDisplays[i] = Instantiate(_ammoDisplayPrefab, _ammoDisplayParent);
                _ammoDisplays[i].Activate();
            }
        }

        private void OnEnable()
        {
            if (_blasterAmmoAmountReadonly == null)
            {
                throw new Exception("Blaster not found");
            }

            _blasterAmmoAmountReadonly.AmmoAmountChanged += AmmoAmountChanged;
        }

        private void OnDisable()
        {
            if (_blasterAmmoAmountReadonly == null)
            {
                throw new Exception("Blaster not found");
            }

            _blasterAmmoAmountReadonly.AmmoAmountChanged -= AmmoAmountChanged;
        }

        private void LateUpdate()
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }

        private void AmmoAmountChanged(int ammoAmount)
        {
            for (int i = 0; i < _ammoDisplays.Length; i++)
            {
                if (i < ammoAmount)
                {
                    _ammoDisplays[i].Activate();
                }
                else
                {
                    _ammoDisplays[i].Deactivate();
                }
            }
        }
    }
}