using PauseManagment;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Gameplay
{
    public class GameplayLifetimeScope : LifetimeScope
    {
        [SerializeField] private GameplayManager _gameplayManager;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_gameplayManager);
            builder.Register<PauseHandler>(Lifetime.Singleton);
        }
    }
}