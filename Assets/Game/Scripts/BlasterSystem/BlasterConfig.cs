using CurrencyManagment;
using ProjectileSystem;
using UnityEngine;

namespace BlasterSystem
{
    [CreateAssetMenu(fileName = "BlasterConfig", menuName = "Configs/Blaster")]
    public class BlasterConfig : ScriptableObject
    {
        [field: SerializeField] public string ID { get; private set; }
        [field: SerializeField] public string DisplayName { get; private set; }
        [field: SerializeField] public int PriorityIndex { get; private set; }
        [field: SerializeField] public WalletOperationData Price { get; private set; }

        [field: Space(25f), SerializeField, Min(1)] public int Damage { get; private set; }
        [field: SerializeField, Range(0f, 10f)] public float Spread { get; private set; }
        [field: SerializeField, Min(0f)] public float ProjectileSpeed { get; private set; }
        [field: SerializeField, Min(1)] public int AmmoAmount { get; private set; }
        [field: SerializeField, Min(0f)] public float ReloadDuration { get; private set; }
        [field: SerializeField, Min(0f)] public float ShotCooldown { get; private set; }

        [field: Space(25f), SerializeField] public Projectile Projectile { get; private set; }
        [field: SerializeField] public Blaster Prefab { get; private set; }
        [field: SerializeField] public PreviewBlaster PreviewPrefab { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public Sprite LockedIcon { get; private set; }
    }
}