using System;
using System.Threading;
using Code.Services;
using Code.SO;
using Code.UI;
using Code.Utils;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer.Unity;

namespace Code.EntryPoint
{
    public class Boot : IAsyncStartable
    {
        private readonly IRemoteContentService _remoteContentService;
        private readonly PopupFactory _popupFactory;
        private readonly Config _cfg;

        public Boot(Config cfg, IRemoteContentService remoteContentService, PopupFactory popupFactory)
        {
            _cfg = cfg;
            _remoteContentService = remoteContentService;
            _popupFactory = popupFactory;
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
                
            }
            catch (Exception e)
            {
                _popupFactory.Create(Constants.DownloadGameConfigError);
                Debug.Log(e.Message);
            }
        }
    }
}