using BlasterSystem;
using UnityEngine;
using VContainer;

namespace Gameplay.UI
{
    public class AmmoAmountDisplay : MonoBehaviour
    {
        [SerializeField] private AmmoDisplay _ammoDisplayPrefab;
        [SerializeField] private RectTransform _ammoDisplayParent;

        private BlasterHolder _blasterHolder;
        private AmmoDisplay[] _ammoDisplays;

        [Inject]
        private void Construct(BlasterHolder blasterHolder)
        {
            _blasterHolder = blasterHolder;
        }

        private void OnEnable()
        {
            CreateAmmoDisplays();
            _blasterHolder.Blaster.AmmoAmountChanged += AmmoAmountChanged;
        }

        private void OnDisable()
        {
            _blasterHolder.Blaster.AmmoAmountChanged -= AmmoAmountChanged;
        }

        private void LateUpdate()
        {
            transform.rotation = Quaternion.identity;
        }

        private void CreateAmmoDisplays()
        {
            if (_ammoDisplays != null && _ammoDisplays.Length > 0)
            {
                for (int i = 0; i < _ammoDisplays.Length; i++)
                {
                    if (_ammoDisplays[i] != null)
                    {
                        Destroy(_ammoDisplays[i].gameObject);
                    }
                }
            }

            int maxAmmo = _blasterHolder.Blaster.MaxAmmoAmount;
            _ammoDisplays = new AmmoDisplay[maxAmmo];

            for (int i = 0; i < maxAmmo; i++)
            {
                _ammoDisplays[i] = Instantiate(_ammoDisplayPrefab, _ammoDisplayParent);
                _ammoDisplays[i].Activate();
            }
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