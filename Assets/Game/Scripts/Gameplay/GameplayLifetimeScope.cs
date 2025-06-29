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
        [SerializeField] private TranslucentImageSource _translucentImageSource;
        [SerializeField] private BlurBackground _blurBackground;
        [SerializeField] private PauseDisplay _pauseDisplay;

        private IContainerBuilder _builder;

        protected override void Configure(IContainerBuilder builder)
        {
            _builder = builder;

            builder.Register<GameplayManager>(Lifetime.Singleton);
            builder.Register<PauseHandler>(Lifetime.Singleton);

            BindStateMachine();
            BindUI();
        }

        private void BindStateMachine()
        {
            _builder.Register<StateMachine>(Lifetime.Singleton);

            _builder.Register<PreGameState>(Lifetime.Singleton);
            _builder.Register<PlayState>(Lifetime.Singleton);
        }

        private void BindUI()
        {
            _builder.RegisterComponent<TranslucentImageSource>(_translucentImageSource);
            _builder.RegisterComponent<BlurBackground>(_blurBackground);
            _builder.RegisterComponent<PauseDisplay>(_pauseDisplay);
        }
    }
}