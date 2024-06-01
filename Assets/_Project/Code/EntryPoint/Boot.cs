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

        public Boot(Config cfg, IRemoteContentService remoteContentService, PopupFactory popupFactory, GameData gameData)
        {
            _cfg = cfg;
            _remoteContentService = remoteContentService;
            _popupFactory = popupFactory;
            _gameData = gameData;
        }

        public async UniTask StartAsync(CancellationToken cancellation)
        {
            Debug.Log("Boot.Start()");

            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                _popupFactory.Create(Constants.InternetNotAvailable);
                return;
            }

            try
            {
                var remoteCfg = await _remoteContentService.LoadRemoteConfig(_cfg.configUrl, cancellation);
                _gameData.remoteConfig = remoteCfg;

                await SceneManager.LoadSceneAsync(Constants.MenuScene);
            }
            catch (Exception e)
            {
                _popupFactory.Create(Constants.DownloadGameConfigError);
                Debug.Log(e.Message);
            }
        }
    }
}