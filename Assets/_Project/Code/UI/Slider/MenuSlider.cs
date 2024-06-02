using System.Linq;
using System.Threading;
using Code.Services;
using Code.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnlimitedScrollUI;
using VContainer;

namespace Code.UI.Slider
{
    public class MenuSlider : MonoBehaviour
    {
        [SerializeField] private Transform _content;
        [SerializeField] private GridUnlimitedScroller _unlimitedScroller;

        private SliderItemFactory _itemFactory;
        private ILevelService _levelService;
        private CancellationTokenSource _cts;
        private IProgressService _progressService;
        private GameData _gameData;


        [Inject]
        public void Construct(SliderItemFactory itemFactory, ILevelService levelService, IProgressService progressService, GameData gameData)
        {
            _itemFactory = itemFactory;
            _levelService = levelService;
            _progressService = progressService;
            _gameData = gameData;
        }

        private void OnEnable()
        {
            SliderItem.OnSelectLevel += SelectLevel;
        }

        private void OnDisable()
        {
            SliderItem.OnSelectLevel -= SelectLevel;
            _cts?.Cancel();
        }

        private void SelectLevel(int id, bool completed)
        {
            _cts.Cancel();

            _levelService.SetCurrentLevel(id);
            var level = _levelService.GetCurrLevel();
            if (completed) _progressService.ResetProgress(level);

            SceneManager.LoadScene(Constants.GamePlayScene);
        }

        public void Run()
        {
            Debug.Log("MenuSlider.Start()");

            _content.gameObject.ClearChildren();
            _cts = new CancellationTokenSource();
            CreateItems();
        }


        private GameObject FetchPrefabById(int id)
        {
            var level = _levelService.GetLevels().First(l => l.id == id);
            return _itemFactory.Create(level, _cts.Token).gameObject;
        }

        private void CreateItems()
        {
            _unlimitedScroller.SetFetcher(FetchPrefabById);
            _unlimitedScroller.Generate(null, _gameData.remoteConfig.levels.Count, (index, iCell) =>
            {
                var regularCell = iCell as RegularCell;
                if (regularCell != null) regularCell.onGenerated?.Invoke(index);
            });


            // foreach (var item in _levelService.GetLevels().Select(level => _itemFactory.Create(level, _cts.Token)))
            // {
            //     item.transform.SetParent(_content, false);
            // }
        }
    }
}