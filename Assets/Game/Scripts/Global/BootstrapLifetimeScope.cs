using CurrencyManagment;
using VContainer;
using VContainer.Unity;

public class BootstrapLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<CurrencyWallet>(Lifetime.Singleton);
    }
}