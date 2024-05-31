using System.Threading;
using Cysharp.Threading.Tasks;

namespace Code.Services
{
    public interface IRemoteContentService
    {
        public UniTask<RemoteConfig> LoadRemoteConfig(string url, CancellationToken cancellation);
    }
}