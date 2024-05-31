using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Services
{
    public interface IRemoteContentService
    {
        public UniTask<RemoteConfig> LoadRemoteConfig(string url, CancellationToken cancellation);
        public UniTask<Texture> LoadRemoteTexture(string url, CancellationToken cancellationToken);
    }
}