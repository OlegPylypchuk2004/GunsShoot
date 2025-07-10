using BulletSystem;
using UnityEngine;

namespace BlasterSystem
{
    [CreateAssetMenu(fileName = "BlasterConfig", menuName = "Configs/Blaster")]
    public class BlasterConfig : ScriptableObject
    {
        [field: SerializeField, Min(1)] public int Damage { get; private set; }
        [field: SerializeField, Range(0f, 10f)] public float Spread { get; private set; }
        [field: SerializeField, Min(1)] public float BulletSpeed { get; private set; }
        [field: SerializeField, Min(1)] public int AmmoAmount { get; private set; }
        [field: SerializeField, Min(0f)] public float ReloadDuration { get; private set; }
        [field: SerializeField, Min(0f)] public float ShotCooldown { get; private set; }
        [field: SerializeField] public BlasterType Type { get; private set; }

        [field: Space(25f), SerializeField] public Bullet Bullet { get; private set; }
        [field: SerializeField] public Blaster Prefab { get; private set; }
        [field: SerializeField] public PreviewBlaster PreviewPrefab { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
    }
}