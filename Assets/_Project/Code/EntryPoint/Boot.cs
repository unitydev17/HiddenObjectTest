using System;
using System.Threading;
using Code.Services;
using Code.SO;
using Code.UI.Popups;
using Code.Utils;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace Code.EntryPoint
{
    [UsedImplicitly]
    public class Boot : IAsyncStartable
    {
        private readonly IRemoteContentService _remoteContentService;
        private readonly PopupFactory _popupFactory;
        private readonly Config _cfg;
        private readonly GameData _gameData;
        private readonly IPersistenceService _persistenceService;

        public Boot(Config cfg, IRemoteContentService remoteContentService, PopupFactory popupFactory, GameData gameData, IPersistenceService persistenceService)
        {
            _cfg = cfg;
            _remoteContentService = remoteContentService;
            _popupFactory = popupFactory;
            _gameData = gameData;
            _persistenceService = persistenceService;
        }

        public async UniTask StartAsync(CancellationToken cancellation)
        {
            Debug.Log("Boot.Start()");

            if (HasNoInternet()) return;

            _persistenceService.LoadPlayerData();

            var result = await LoadRemoteConfig(cancellation);

            if (result) SceneManager.LoadScene(Constants.MenuScene);
        }

        private async UniTask<bool> LoadRemoteConfig(CancellationToken cancellation)
        {
            try
            {
                var remoteCfg = await _remoteContentService.LoadRemoteConfig(_cfg.configUrl, cancellation);
                _gameData.remoteConfig = remoteCfg;
                return true;
            }
            catch (Exception e)
            {
                _popupFactory.Create(Constants.DownloadGameConfigError);
                Debug.Log(e.Message);
            }

            return false;
        }

        private bool HasNoInternet()
        {
            if (Application.internetReachability != NetworkReachability.NotReachable) return false;
            _popupFactory.Create(Constants.InternetNotAvailable);
            return true;
        }
    }
}