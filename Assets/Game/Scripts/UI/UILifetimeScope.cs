using LeTai.Asset.TranslucentImage;
using UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class UILifetimeScope : LifetimeScope
{
    [SerializeField] private TranslucentImageSource _translucentImageSource;
    [SerializeField] private BlurBackground _blurBackground;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent<TranslucentImageSource>(_translucentImageSource);
        builder.RegisterComponent<BlurBackground>(_blurBackground);
    }
}