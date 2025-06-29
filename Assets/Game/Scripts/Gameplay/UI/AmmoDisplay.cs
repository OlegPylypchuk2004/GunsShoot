using BlasterSystem;
using TMPro;
using UnityEngine;

namespace Gameplay.UI
{
    public class AmmoDisplay : MonoBehaviour
    {
        [SerializeField] private Blaster _blaster;
        [SerializeField] private TextMeshProUGUI _textMesh;

        private void LateUpdate()
        {
            if (_blaster.State == BlasterState.Reloading)
            {
                _textMesh.text = $"{Mathf.Round(_blaster.ReloadTime * 100f) / 100f}s";
            }
            else
            {
                _textMesh.text = $"{_blaster.Ammo}";
            }

            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }
}