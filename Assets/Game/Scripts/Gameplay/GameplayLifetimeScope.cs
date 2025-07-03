using BlasterSystem;
using CameraManagment;
using Gameplay.States;
using Gameplay.UI;
using HealthSystem;
using LeTai.Asset.TranslucentImage;
using ObstacleSystem;
using Patterns.StateMachine;
using PauseManagment;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Gameplay
{
    public class GameplayLifetimeScope : LifetimeScope
    {
        [Header("Gameplay"), SerializeField] private BlasterController _blasterController;
        [SerializeField] private CameraShaker _cameraShaker;

        [Header("UI"), SerializeField] private TranslucentImageSource _translucentImageSource;
        [SerializeField] private BlurBackground _blurBackground;
        [SerializeField] private PauseDisplay _pauseDisplay;
        [SerializeField] private AmmoAmountDisplay _ammoAmountDisplay;

        private IContainerBuilder _builder;

        protected override void Configure(IContainerBuilder builder)
        {
            _builder = builder;

            RegisterSystems();
            RegisterComponents();
            RegisterStateMachine();
            RegisterUI();
        }

        private void RegisterSystems()
        {
            _builder.RegisterEntryPoint<GameplayManager>(Lifetime.Singleton);
            _builder.Register<PauseHandler>(Lifetime.Singleton);
            _builder.Register<BlasterHolder>(Lifetime.Singleton);
            _builder.Register<ObstacleContainer>(Lifetime.Singleton);
            _builder.Register<HealthManager>(Lifetime.Singleton);
        }

        private void RegisterComponents()
        {
            _builder.RegisterComponent(_blasterController);
            _builder.RegisterComponent(_cameraShaker);
        }

        private void RegisterStateMachine()
        {
            _builder.Register<StateMachine>(Lifetime.Singleton);

            _builder.Register<PreGameplayState>(Lifetime.Singleton);
            _builder.Register<GameplayState>(Lifetime.Singleton);
        }

        private void RegisterUI()
        {
            _builder.RegisterComponent<BlurBackground>(_blurBackground);
            _builder.RegisterComponent<PauseDisplay>(_pauseDisplay);
            _builder.RegisterComponent<AmmoAmountDisplay>(_ammoAmountDisplay);
        }
    }
}