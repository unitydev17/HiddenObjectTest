using Code;
using Code.EntryPoint;
using Code.Services;
using Code.SO;
using Code.UI.Popups;
using Code.Utils;
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
        RegisterExitPoint(builder);
        RegisterScriptableObjects(builder);
        RegisterServices(builder);
        RegisterFactories(builder);

        builder.Register<GameData>(Lifetime.Singleton);
        builder.Register<PlayerData>(Lifetime.Singleton);
    }

    private void RegisterExitPoint(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<PersistenceControl>();
    }

    private void RegisterFactories(IContainerBuilder builder)
    {
        builder.Register<PopupFactory>(Lifetime.Scoped).WithParameter(typeof(Popup), _popupPrefab);
    }

    private static void RegisterServices(IContainerBuilder builder)
    {
        builder.Register<IRemoteContentService, RemoteContentService>(Lifetime.Scoped);
        builder.Register<ICacheService, CacheService>(Lifetime.Scoped);
        builder.Register<IPersistenceService, PersistenceService>(Lifetime.Scoped);
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