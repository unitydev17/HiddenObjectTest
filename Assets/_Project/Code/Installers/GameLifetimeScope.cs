using Code.EntryPoint;
using Code.Services;
using Code.SO;
using Code.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private Config _cfg;
    [SerializeField] private Popup _popupPrefab;

    protected override void Configure(IContainerBuilder builder)
    {
        RegisterEntryPoint(builder);
        RegisterScriptableObjects(builder);
        RegisterServices(builder);
        // RegisterPrefabs(builder);
        RegisterFactories(builder);
    }

    private void RegisterFactories(IContainerBuilder builder)
    {
        builder.Register<PopupFactory>(Lifetime.Scoped).WithParameter(typeof(Popup), _popupPrefab);
    }

    // private void RegisterPrefabs(IContainerBuilder builder)
    // {
    //     builder.RegisterComponentInNewPrefab(_errorPopup, Lifetime.Singleton);
    // }

    private static void RegisterServices(IContainerBuilder builder)
    {
        builder.Register<IRemoteContentService, RemoteContentService>(Lifetime.Scoped);
    }

    private void RegisterScriptableObjects(IContainerBuilder builder)
    {
        builder.RegisterInstance(_cfg);
    }

    private static void RegisterEntryPoint(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<Boot>();
    }
}