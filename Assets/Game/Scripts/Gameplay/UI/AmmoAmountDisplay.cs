using BlasterSystem;
using TMPro;
using UnityEngine;
using VContainer;

namespace Gameplay.UI
{
    public class AmmoAmountDisplay : MonoBehaviour
    {
        [SerializeField] private AmmoDisplay _ammoDisplayPrefab;
        [SerializeField] private RectTransform _ammoDisplayParent;

        private BlasterHolder _blasterHolder;
        private PhysicalProjectilesBlaster _previousBlaster;
        private AmmoDisplay[] _ammoDisplays;

        [Inject]
        private void Construct(BlasterHolder blasterHolder)
        {
            _blasterHolder = blasterHolder;
        }

        private void OnEnable()
        {
            _blasterHolder.BlasterChanged += OnBlasterChanged;

            if (_blasterHolder.Blaster != null)
            {
                _previousBlaster = _blasterHolder.Blaster;

                CreateAmmoDisplays();

                _previousBlaster.AmmoAmountChanged += AmmoAmountChanged;
                _previousBlaster.ReloadTimeChanged += OnReloadTimeChanged;
            }
        }

        private void OnDisable()
        {
            _blasterHolder.BlasterChanged -= OnBlasterChanged;

            if (_previousBlaster != null)
            {
                _previousBlaster.AmmoAmountChanged -= AmmoAmountChanged;
                _previousBlaster.ReloadTimeChanged -= OnReloadTimeChanged;
                _previousBlaster = null;
            }
        }

        private void LateUpdate()
        {
            transform.rotation = Quaternion.identity;
        }

        private void OnBlasterChanged(PhysicalProjectilesBlaster blaster)
        {
            if (_previousBlaster != null)
            {
                _previousBlaster.AmmoAmountChanged -= AmmoAmountChanged;
                _previousBlaster.ReloadTimeChanged -= OnReloadTimeChanged;
            }

            if (blaster != null)
            {
                CreateAmmoDisplays();

                blaster.AmmoAmountChanged += AmmoAmountChanged;
                blaster.ReloadTimeChanged += OnReloadTimeChanged;

                _previousBlaster = blaster;
            }
            else
            {
                _previousBlaster = null;
            }
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

        private void OnReloadTimeChanged(float currentTime, float targetTime)
        {
            if (targetTime <= 0f)
            {
                return;
            }

            currentTime = Mathf.Clamp(currentTime, 0f, targetTime);
            float progress = currentTime / targetTime;
            int activeAmmoDisplaysAmount = _ammoDisplays.Length - Mathf.RoundToInt(_ammoDisplays.Length * progress);

            for (int i = 0; i < _ammoDisplays.Length; i++)
            {
                if (i < activeAmmoDisplaysAmount)
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