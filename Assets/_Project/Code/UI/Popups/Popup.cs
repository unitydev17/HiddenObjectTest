using System;
using TMPro;
using UnityEngine;

namespace Code.UI.Popups
{
    public class Popup : MonoBehaviour
    {
        public TMP_Text _text;
        private Action _callback;

        public void SetCallback(Action callback)
        {
            _callback = callback;
        }

        public void SetMessage(string message)
        {
            _text.text = message;
        }

        public void OnRetry()
        {
            _callback?.Invoke();
            Destroy(gameObject);
        }
    }
}