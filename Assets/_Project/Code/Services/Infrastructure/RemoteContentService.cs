using System;
using System.IO;
using System.Threading;
using Code.SO;
using Code.Utils;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Code.Services
{
    public class RemoteContentService : IRemoteContentService
    {
        private readonly Config _cfg;
        private readonly ICacheService _cacheService;

        public RemoteContentService(Config cfg, ICacheService cacheService)
        {
            _cfg = cfg;
            _cacheService = cacheService;
        }

        public async UniTask<RemoteConfig> LoadRemoteConfig(string url, CancellationToken cancellation)
        {
            if (url == null) throw new Exception(Constants.UrlEmpty);

            var request = UnityWebRequest.Get(url);
            request.timeout = _cfg.timeout;

            var asyncOperation = await request.SendWebRequest().WithCancellation(cancellation);
            if (asyncOperation.result == UnityWebRequest.Result.Success)
            {
                return JsonUtility.FromJson<RemoteConfig>(request.downloadHandler.text);
            }

            throw new Exception(request.error);
        }


        public async UniTask<Texture> LoadRemoteTexture(string url, CancellationToken cancellation)
        {
            if (url == null) throw new Exception(Constants.UrlEmpty);

            var path = Path.Combine(Application.persistentDataPath, Path.GetFileName(url));
            if (_cacheService.IsCached(path))
            {
                Debug.Log("RemoteContentService.LoadRemoteTexture(): return cached texture");
                return _cacheService.LoadFromCache(path);
            }

            var request = UnityWebRequestTexture.GetTexture(url);
            request.timeout = _cfg.timeout;


            var asyncOperation = await request.SendWebRequest().WithCancellation(cancellation);
            if (asyncOperation.result != UnityWebRequest.Result.Success) throw new Exception(request.error);

            Debug.Log("RemoteContentService.LoadRemoteTexture(): downloaded texture");


            _cacheService.CacheTexture(request.downloadHandler.data, path);
            return _cacheService.LoadFromCache(path);
        }
    }
}