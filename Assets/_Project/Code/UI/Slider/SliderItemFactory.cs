using System;
using System.Threading;
using Code.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.UI.Slider
{
    public class SliderItemFactory
    {
        private readonly IObjectResolver _resolver;
        private readonly SliderItem _prefab;
        private readonly IRemoteContentService _remoteContentService;
        private readonly IProgressService _progressService;

        public SliderItemFactory(IObjectResolver resolver, SliderItem prefab, IRemoteContentService remoteContentService, IProgressService progressService)
        {
            _resolver = resolver;
            _prefab = prefab;
            _remoteContentService = remoteContentService;
            _progressService = progressService;
        }

        private SliderItem Create()
        {
            return _resolver.Instantiate(_prefab).GetComponent<SliderItem>();
        }

        public SliderItem Create(Level level, CancellationToken cancelToken)
        {
            var item = Create();
            item.SetId(level.id);
            item.SetName(level.imageName);

            var (progress, completed) = _progressService.GetProgress(level);
            item.SetProgress(completed ? string.Empty : progress.ToString());
            item.SetState(completed ? SliderItem.State.Completed : SliderItem.State.Loading);

            LoadTexture(level, cancelToken, item).Forget();

            return item;
        }

        private async UniTaskVoid LoadTexture(Level level, CancellationToken cancelToken, SliderItem item)
        {
            try
            {
                var tex = await _remoteContentService.LoadRemoteTexture(level.imageUrl, cancelToken);
                item.SetTexture(tex);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                if (item) item.SetState(SliderItem.State.Unavailable);
            }
        }
    }
}