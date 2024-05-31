using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Code.Services
{
    public class RemoteContentService : IRemoteContentService
    {
        public async UniTask<RemoteConfig> LoadRemoteConfig(string url, CancellationToken cancellation)
        {
            return await ReadJsonUrl(url, cancellation);
        }

        private async UniTask<RemoteConfig> ReadJsonUrl(string url, CancellationToken cancellation)
        {
            var request = UnityWebRequest.Get(url);
            request.timeout = 10;
            var asyncOperation = await request.SendWebRequest().WithCancellation(cancellation);
            if (asyncOperation.result == UnityWebRequest.Result.Success)
            {
                return JsonUtility.FromJson<RemoteConfig>(request.downloadHandler.text);
            }

            throw new Exception(request.error);
        }
    }
}