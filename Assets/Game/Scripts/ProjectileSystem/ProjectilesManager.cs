using BlasterSystem;

namespace ProjectileSystem
{
    public abstract class ProjectilesManager
    {
        public abstract void Initialize(BlasterConfig blasterConfig);
        public abstract PhysicalProjectile CreatePhysicalProjectile();
    }
}