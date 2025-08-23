using UnityEngine;

namespace BlasterSystem
{
    public class PreviewBlaster : MonoBehaviour
    {
        [field: SerializeField] public BlasterConfig Config { get; private set; }
        [SerializeField] private MeshRenderer[] _meshRenderers;
        [SerializeField] private Material _notAvailableMaterial;

        private Material _defaultMaterial;

        private void Awake()
        {
            if (_meshRenderers == null || _meshRenderers.Length == 0)
            {
                return;
            }

            _defaultMaterial = _meshRenderers[0].material;
        }

        public void UpdateDisplay(bool isAvailable)
        {
            if (isAvailable)
            {
                if (_defaultMaterial == null)
                {
                    return;
                }

                foreach (MeshRenderer meshRenderer in _meshRenderers)
                {
                    meshRenderer.material = _defaultMaterial;
                }
            }
            else
            {
                foreach (MeshRenderer meshRenderer in _meshRenderers)
                {
                    meshRenderer.material = _notAvailableMaterial;
                }
            }
        }
    }
}