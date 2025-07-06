using SceneManagment;
using VContainer;
using VContainer.Unity;

namespace Menu
{
    public class MenuLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<SceneLoader>(Lifetime.Singleton);
        }
    }
}