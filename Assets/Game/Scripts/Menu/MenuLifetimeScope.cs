using CurrencyManagment;
using SceneManagment;
using VContainer;
using VContainer.Unity;

namespace Menu
{
    public class MenuLifetimeScope : LifetimeScope
    {
        private IContainerBuilder _builder;

        protected override void Configure(IContainerBuilder builder)
        {
            _builder = builder;

            RegiterSceneLoader();
            RegisterCurrencyWallet();
        }

        private void RegiterSceneLoader()
        {
            _builder.Register<SceneLoader>(Lifetime.Singleton);
        }

        private void RegisterCurrencyWallet()
        {
            _builder.Register<CurrencyWallet>(Lifetime.Singleton);
        }
    }
}