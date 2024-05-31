using System;
using System.Threading;
using Code.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Code.UI
{
    public class UI : MonoBehaviour
    {
        [SerializeField] private RawImage _image;


        private GameData _gameData;
        private IRemoteContentService _remoteContentService;
        private CancellationTokenSource _cts;

        [Inject]
        public void Construct(GameData gameData, IRemoteContentService remoteContentService)
        {
            _gameData = gameData;
            _remoteContentService = remoteContentService;
        }

        public async void TestAction()
        {
            await LoadTexture();
        }

        private async UniTask LoadTexture()
        {
            Debug.Log("UI.TestAction()");

            var url = _gameData.remoteConfig.levels[0].imageUrl;

            try
            {
                _cts = new CancellationTokenSource();
                _image.texture = await _remoteContentService.LoadRemoteTexture(url, _cts.Token);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }


        private void OnDestroy()
        {
            _cts.Cancel();
        }
    }
}