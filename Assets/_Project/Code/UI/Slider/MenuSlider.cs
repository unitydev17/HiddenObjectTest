using System.Threading;
using Code.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace Code.UI.Slider
{
    public class MenuSlider : MonoBehaviour
    {
        [SerializeField] private Transform _content;

        private GameData _gameData;
        private SliderItemFactory _itemFactory;
        private CancellationTokenSource _cts;
        private PlayerData _playerData;


        [Inject]
        public void Construct(GameData gameData, PlayerData playerData, SliderItemFactory itemFactory)
        {
            _gameData = gameData;
            _playerData = playerData;
            _itemFactory = itemFactory;
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

        private void SelectLevel(int id)
        {
            _playerData.currentId = id;
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
            foreach (var level in _gameData.remoteConfig.levels)
            {
                var item = _itemFactory.Create(level, _cts.Token);
                item.transform.SetParent(_content, false);
            }
        }
    }
}