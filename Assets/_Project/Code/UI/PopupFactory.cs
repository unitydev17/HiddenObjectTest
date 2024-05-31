using VContainer;
using VContainer.Unity;

namespace Code.UI
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

        public void Create(string message)
        {
            var popup = Create();
            popup.SetMessage(message);
            popup.gameObject.SetActive(true);
        }
    }
}