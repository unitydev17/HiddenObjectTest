using TMPro;
using UnityEngine;

namespace Code.UI.Popups
{
    public class Popup : MonoBehaviour
    {
        public TMP_Text _text;

        public void SetMessage(string message)
        {
            _text.text = message;
        }
    }
}