using UnityEngine;

namespace BulletSystem
{
    [CreateAssetMenu(fileName = "BulletConfig", menuName = "Configs/Bullet")]
    public class BulletConfig : ScriptableObject
    {
        [field: SerializeField] public Bullet Prefab { get; private set; }
    }
}