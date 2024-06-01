using Code;
using Code.EntryPoint;
using Code.Services;
using Code.SO;
using Code.UI.Popups;
using Code.Utils;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class AppLifetimeScope : LifetimeScope
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
        RegisterDataObjects(builder);
    }

    private static void RegisterDataObjects(IContainerBuilder builder)
    {
        builder.Register<GameData>(Lifetime.Singleton);
        builder.Register<PlayerData>(Lifetime.Singleton);
    }

    private void RegisterFactories(IContainerBuilder builder)
    {
        builder.Register<PopupFactory>(Lifetime.Scoped).WithParameter(typeof(Popup), _popupPrefab);
    }

    private static void RegisterServices(IContainerBuilder builder)
    {
        builder.Register<IRemoteContentService, RemoteContentService>(Lifetime.Scoped);
        builder.Register<ICacheService, CacheService>(Lifetime.Scoped);
        builder.Register<IPlayerDataService, PlayerDataService>(Lifetime.Scoped);
        builder.Register<IPersistenceService, PersistenceService>(Lifetime.Scoped);
        builder.Register<IProgressService, ProgressService>(Lifetime.Scoped);
        builder.Register<ILevelService, LevelService>(Lifetime.Scoped);
    }

    private void RegisterScriptableObjects(IContainerBuilder builder)
    {
        builder.RegisterInstance(_cfg);
    }

    private static void RegisterEntryPoint(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<Boot>();
    }
    
    private static void RegisterExitPoint(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<PersistenceControl>();
    }
}