using BulletSystem;
using UnityEngine;

namespace BlasterSystem
{
    [CreateAssetMenu(fileName = "BlasterConfig", menuName = "Configs/Blaster")]
    public class BlasterConfig : ScriptableObject
    {
        [field: SerializeField] public BulletConfig Bullet { get; private set; }
        [field: SerializeField, Min(1)] public int Ammo { get; private set; }
        [field: SerializeField, Min(0f)] public float ReloadDuration { get; private set; }
        [field: SerializeField, Min(0f)] public float ShotCooldown { get; private set; }
    }
}