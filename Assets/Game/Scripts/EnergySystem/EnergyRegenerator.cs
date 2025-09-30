using VContainer.Unity;

namespace EnergySystem
{
    public class EnergyRegenerator : ITickable
    {
        private EnergyManager _energyManager;

        public EnergyRegenerator(EnergyManager energyManager)
        {
            _energyManager = energyManager;
        }

        public void Tick()
        {
            _energyManager.Update();
        }
    }
}