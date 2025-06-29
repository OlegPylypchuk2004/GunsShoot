using LeTai.Asset.TranslucentImage;
using PauseManagment;
using UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Gameplay
{
    public class GameplayLifetimeScope : LifetimeScope
    {
        [SerializeField] private GameplayManager _gameplayManager;
        [SerializeField] private TranslucentImageSource _translucentImageSource;
        [SerializeField] private BlurBackground _blurBackground;
        [SerializeField] private PauseDisplay _pauseDisplay;

        private IContainerBuilder _builder;

        protected override void Configure(IContainerBuilder builder)
        {
            _builder = builder;

            builder.RegisterComponent(_gameplayManager);
            builder.Register<PauseHandler>(Lifetime.Singleton);

            BindUI();
        }

        private void BindUI()
        {
            _builder.RegisterComponent<TranslucentImageSource>(_translucentImageSource);
            _builder.RegisterComponent<BlurBackground>(_blurBackground);
            _builder.RegisterComponent<PauseDisplay>(_pauseDisplay);
        }
    }
}