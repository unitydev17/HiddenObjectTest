using System;
using System.IO;
using System.Threading;
using Code.Utils;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Code.Services
{
    public class RemoteContentService : IRemoteContentService
    {
        private const int Timeout = 10;

        public async UniTask<RemoteConfig> LoadRemoteConfig(string url, CancellationToken cancellation)
        {
            if (url == null) throw new Exception(Constants.UrlEmpty);

            var request = UnityWebRequest.Get(url);
            request.timeout = Timeout;

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

            // check for cached textures

            var path = Path.Combine(Application.persistentDataPath, Path.GetFileName(url));
            if (IsExistInCache(path))
            {
                Debug.Log("RemoteContentService.LoadRemoteTexture(): return cached texture");
                return LoadFromCache(path);
            }

            // load remotely

            var request = UnityWebRequestTexture.GetTexture(url);
            request.timeout = Timeout;

            var asyncOperation = await request.SendWebRequest().WithCancellation(cancellation);
            if (asyncOperation.result != UnityWebRequest.Result.Success) throw new Exception(request.error);
            {
                Debug.Log("RemoteContentService.LoadRemoteTexture(): downloaded texture");
                CacheTexture(request, path);
                return LoadFromCache(path);
            }
        }

        private static bool IsExistInCache(string cachePath)
        {
            return File.Exists(cachePath);
        }

        private static Texture2D LoadFromCache(string cachePath)
        {
            var bytes = File.ReadAllBytes(cachePath);
            var texture = new Texture2D(2, 2);
            texture.LoadImage(bytes);
            texture.Apply();
            return texture;
        }

        private static void CacheTexture(UnityWebRequest request, string cachePath)
        {
            var bytes = request.downloadHandler.data;
            File.WriteAllBytes(cachePath, bytes);
        }
    }
}