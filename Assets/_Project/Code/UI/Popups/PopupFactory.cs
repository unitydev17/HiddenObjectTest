using System;
using VContainer;
using VContainer.Unity;

namespace Code.UI.Popups
{
    public class PopupFactory
    {
        private readonly IObjectResolver _resolver;
        private readonly Popup _prefab;

        public PopupFactory(IObjectResolver resolver, Popup prefab)
        {
            _resolver = resolver;
            _prefab = prefab;
        }

        private Popup Create()
        {
            return _resolver.Instantiate(_prefab.gameObject).GetComponent<Popup>();
        }

        public void Create(string message, Action callback = null)
        {
            var popup = Create();
            popup.SetMessage(message);
            popup.SetCallback(callback);
            popup.gameObject.SetActive(true);
        }
    }
}