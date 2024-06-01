using System;
using System.Linq;
using System.Threading;
using Code;
using Code.Services;
using Code.Utils;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VContainer;
using Random = UnityEngine.Random;

public class GamePlay : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Button _backButton;
    [SerializeField] private TMP_Text _progress;
    [SerializeField] private RawImage _image;


    private IRemoteContentService _remoteContentService;
    private PlayerData _playerData;
    private GameData _gameData;
    private CancellationTokenSource _cts;

    [Inject]
    public void Construct(IRemoteContentService remoteContentService, PlayerData playerData, GameData gameData)
    {
        _remoteContentService = remoteContentService;
        _playerData = playerData;
        _gameData = gameData;
    }

    private void OnEnable()
    {
        _backButton.onClick.AddListener(ClickBackProcess);
        var currLevel = _gameData.remoteConfig.levels.First(level => level.id == _playerData.currentId);

        _cts = new CancellationTokenSource();
        try
        {
            _remoteContentService.LoadRemoteTexture(currLevel.imageUrl, _cts.Token).ContinueWith(tex => _image.texture = tex);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    private void OnDisable()
    {
        _backButton.onClick.RemoveAllListeners();
        _cts.Cancel();
    }

    private void ClickBackProcess()
    {
        SceneManager.LoadScene(Constants.MenuScene);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _progress.text = string.Format(Constants.Progress, Random.Range(0, 10));
    }
}