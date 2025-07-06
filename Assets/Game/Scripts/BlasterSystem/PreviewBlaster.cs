using UnityEngine;

namespace BlasterSystem
{
    public class PreviewBlaster : MonoBehaviour
    {
        [field: SerializeField] public BlasterConfig Config { get; private set; }
    }
}