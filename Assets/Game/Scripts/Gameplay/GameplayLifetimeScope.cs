using BlasterSystem;
using Gameplay.States;
using LeTai.Asset.TranslucentImage;
using Patterns.StateMachine;
using PauseManagment;
using UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Gameplay
{
    public class GameplayLifetimeScope : LifetimeScope
    {
        [Header("Gameplay"), SerializeField] private Camera _camera;
        [SerializeField] private BlasterController _blasterController;

        [Header("UI"), SerializeField] private TranslucentImageSource _translucentImageSource;
        [SerializeField] private BlurBackground _blurBackground;
        [SerializeField] private PauseDisplay _pauseDisplay;

        private IContainerBuilder _builder;

        protected override void Configure(IContainerBuilder builder)
        {
            _builder = builder;

            _builder.RegisterEntryPoint<GameplayManager>(Lifetime.Singleton);
            _builder.Register<PauseHandler>(Lifetime.Singleton);

            _builder.RegisterInstance(_camera);
            _builder.RegisterComponent(_blasterController);

            RegisterStateMachine();
            RegisterUI();
        }

        private void RegisterStateMachine()
        {
            _builder.Register<StateMachine>(Lifetime.Singleton);

            _builder.Register<PreGameplayState>(Lifetime.Singleton);
            _builder.Register<GameplayState>(Lifetime.Singleton);
        }

        private void RegisterUI()
        {
            _builder.RegisterComponent<TranslucentImageSource>(_translucentImageSource);
            _builder.RegisterComponent<BlurBackground>(_blurBackground);
            _builder.RegisterComponent<PauseDisplay>(_pauseDisplay);
        }
    }
}