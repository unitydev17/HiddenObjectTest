using Code.UI.Slider;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class MenuLifetimeScope : LifetimeScope
{
    [SerializeField] private SliderItem _itemPrefab;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<MenuSlider>();
        builder.Register<SliderItemFactory>(Lifetime.Scoped).WithParameter(typeof(SliderItem), _itemPrefab);
    }
}