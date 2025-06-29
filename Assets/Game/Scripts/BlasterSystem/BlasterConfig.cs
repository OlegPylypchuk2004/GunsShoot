using BulletSystem;
using UnityEngine;

namespace BlasterSystem
{
    [CreateAssetMenu(fileName = "BlasterConfig", menuName = "Configs/Blaster")]
    public class BlasterConfig : ScriptableObject
    {
        [field: SerializeField] public BulletConfig Bullet { get; private set; }
    }
}