using System;
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
using VContainer.Unity;

public class GamePlay : MonoBehaviour, IPointerClickHandler, IInitializable, IDisposable
{
    [SerializeField] private Bounce _bounce;
    [SerializeField] private Button _backButton;
    [SerializeField] private TMP_Text _progress;
    [SerializeField] private RawImage _image;

    private IRemoteContentService _remoteContentService;
    private IProgressService _progressService;
    private ILevelService _levelService;


    private CancellationTokenSource _cts;
    private Level _level;


    [Inject]
    public void Construct(IRemoteContentService remoteContentService, IProgressService progressService, ILevelService levelService)
    {
        _remoteContentService = remoteContentService;
        _progressService = progressService;
        _levelService = levelService;
    }

    public void Initialize()
    {
        _level = _levelService.GetCurrLevel();

        var (progress, completed) = _progressService.GetProgress(_level);
        UpdateView(progress, completed);

        _cts = new CancellationTokenSource();
        try
        {
            _remoteContentService.LoadRemoteTexture(_level.imageUrl, _cts.Token).ContinueWith(tex => _image.texture = tex);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public void Dispose()
    {
        _cts?.Dispose();
    }

    private void OnEnable()
    {
        _backButton.onClick.AddListener(ClickBackProcess);
    }

    private void OnDisable()
    {
        _backButton.onClick.RemoveAllListeners();
    }

    private static void ClickBackProcess()
    {
        SceneManager.LoadScene(Constants.MenuScene);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _bounce.Run(eventData.position);

        _progressService.IncreaseCounter(_level);

        var (progress, completed) = _progressService.GetProgress(_level);
        UpdateView(progress, completed);

        if (completed) ClickBackProcess();
    }

    private void UpdateView(int progress, bool completed)
    {
        _progress.text = completed ? string.Empty : string.Format(Constants.Progress, progress);
    }
}