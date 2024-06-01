using System.Linq;
using System.Threading;
using Code.Services;
using Code.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace Code.UI.Slider
{
    public class MenuSlider : MonoBehaviour
    {
        [SerializeField] private Transform _content;

        private SliderItemFactory _itemFactory;
        private ILevelService _levelService;
        private CancellationTokenSource _cts;
        private IProgressService _progressService;


        [Inject]
        public void Construct(SliderItemFactory itemFactory, ILevelService levelService, IProgressService progressService)
        {
            _itemFactory = itemFactory;
            _levelService = levelService;
            _progressService = progressService;
        }

        private void OnDestroy()
        {
            _cts?.Cancel();
        }

        private void OnEnable()
        {
            SliderItem.OnSelectLevel += SelectLevel;
        }

        private void OnDisable()
        {
            SliderItem.OnSelectLevel -= SelectLevel;
        }

        private void SelectLevel(int id, bool completed)
        {
            _cts.Cancel();

            _levelService.SetCurrentLevel(id);
            var level = _levelService.GetCurrLevel();
            if (completed) _progressService.ResetProgress(level);

            SceneManager.LoadScene(Constants.GamePlayScene);
        }

        private void Start()
        {
            Debug.Log("MenuSlider.Start()");

            _content.gameObject.ClearChildren();
            _cts = new CancellationTokenSource();
            CreateItems();
        }

        private void CreateItems()
        {
            foreach (var item in _levelService.GetLevels().Select(level => _itemFactory.Create(level, _cts.Token)))
            {
                item.transform.SetParent(_content, false);
            }
        }
    }
}