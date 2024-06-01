using Code.Utils;
using Newtonsoft.Json;
using UnityEngine;

namespace Code.Services
{
    public class PersistenceService : IPersistenceService
    {
        private readonly IPlayerDataService _playerDataService;

        public PersistenceService(IPlayerDataService playerDataService)
        {
            _playerDataService = playerDataService;
        }

        public void SavePlayerData()
        {
            var playerData = _playerDataService.GetData();
            var data = JsonConvert.SerializeObject(playerData);

            PlayerPrefs.SetString(nameof(PlayerData), data);
            PlayerPrefs.Save();
            Debug.Log($"PersistenceService.SavePlayerData {data}");
        }

        public void LoadPlayerData()
        {
            Debug.Log("PersistenceService.SavePlayerData");

            const string key = nameof(PlayerData);
            if (!PlayerPrefs.HasKey(key))
            {
                Debug.Log($"key not exists");
                return;
            }

            var data = PlayerPrefs.GetString(key);
            if (data.IsNullOrWhiteSpace())
            {
                Debug.Log($"data empty");
                return;
            }

            _playerDataService.SetPlayerData(JsonConvert.DeserializeObject<PlayerData>(data));
            Debug.Log($"data: {data}");
        }
    }
}