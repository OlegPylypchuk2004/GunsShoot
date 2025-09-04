using CurrencyManagment;
using EnergySystem;
using SceneManagment;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Menu
{
    public class MenuLifetimeScope : LifetimeScope
    {
        [SerializeField] private EnergySystemConfig _energySystemConfig;

        private IContainerBuilder _builder;

        protected override void Configure(IContainerBuilder builder)
        {
            _builder = builder;

            RegiterSceneLoader();
            RegisterCurrencyWallet();
            RegisterEnergySystem();
        }

        private void RegiterSceneLoader()
        {
            _builder.Register<SceneLoader>(Lifetime.Singleton);
        }

        private void RegisterCurrencyWallet()
        {
            _builder.Register<CurrencyWallet>(Lifetime.Singleton);
        }

        private void RegisterEnergySystem()
        {
            _builder.Register<EnergyManager>(Lifetime.Singleton)
                .WithParameter(_energySystemConfig);

            _builder.RegisterEntryPoint<EnergyRegenerator>(Lifetime.Singleton);
        }
    }
}