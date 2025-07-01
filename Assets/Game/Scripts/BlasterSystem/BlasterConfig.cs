using BulletSystem;
using UnityEngine;

namespace BlasterSystem
{
    [CreateAssetMenu(fileName = "BlasterConfig", menuName = "Configs/Blaster")]
    public class BlasterConfig : ScriptableObject
    {
        [field: SerializeField] public Bullet Bullet { get; private set; }
        [field: SerializeField, Min(1)] public int Damage { get; private set; }
        [field: SerializeField, Min(1)] public float BulletSpeed { get; private set; }
        [field: SerializeField, Min(1)] public int Ammo { get; private set; }
        [field: SerializeField, Min(0f)] public float ReloadDuration { get; private set; }
        [field: SerializeField, Min(0f)] public float ShotCooldown { get; private set; }
    }
}