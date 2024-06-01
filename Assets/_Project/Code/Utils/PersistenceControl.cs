using Code.Services;
using UnityEngine;
using VContainer;

namespace Code.Utils
{
    public class PersistenceControl : MonoBehaviour
    {
        private IPersistenceService _persistenceService;

        [Inject]
        public void Construct(IPersistenceService persistenceService)
        {
            _persistenceService = persistenceService;
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (!hasFocus) _persistenceService.SavePlayerData();
        }
    }
}