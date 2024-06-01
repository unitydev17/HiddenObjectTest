using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Slider
{
    public class SliderItem : MonoBehaviour
    {
        public static event Action<int, bool> OnSelectLevel;


        public enum State
        {
            Loading,
            Unavailable,
            Loaded,
            Completed
        }

        [SerializeField] private Button _button;
        [SerializeField] private RawImage _image;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _progress;
        [SerializeField] private GameObject _loaded;
        [SerializeField] private GameObject _completed;
        [SerializeField] private GameObject _unavailable;
        [SerializeField] private GameObject _loading;

        private int _id;
        private State _state;

        private void UpdateState()
        {
            _completed.SetActive(false);
            _loading.SetActive(false);
            _loaded.SetActive(false);
            _unavailable.SetActive(false);

            switch (_state)
            {
                case State.Loading:
                    _loading.SetActive(true);
                    break;
                case State.Unavailable:
                    _unavailable.SetActive(true);
                    break;
                case State.Loaded:
                    _loaded.SetActive(true);
                    break;
                case State.Completed:
                    _completed.SetActive(true);
                    _loaded.SetActive(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void SetId(int id)
        {
            _id = id;
        }

        public void SetTexture(Texture tex)
        {
            _image.texture = tex;
            if (_state != State.Completed) SetState(State.Loaded);
        }

        public void SetName(string value) => _name.text = value;
        public void SetProgress(string value) => _progress.text = value;

        protected void OnEnable()
        {
            _button.onClick.AddListener(ClickProcess);
        }

        protected void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }

        private void ClickProcess()
        {
            if (_state == State.Unavailable || _state == State.Loading) return;
            var completed = _state == State.Completed;

            OnSelectLevel?.Invoke(_id, completed);

            Debug.Log($"SliderButton.Click event itemId={_id}");
        }

        public void SetState(State state)
        {
            _state = state;
            UpdateState();
        }
    }
}