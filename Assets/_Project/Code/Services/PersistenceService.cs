using Code.Utils;
using UnityEngine;

namespace Code.Services
{
    public class PersistenceService : IPersistenceService
    {
        private PlayerData _playerData;

        public PersistenceService(PlayerData playerData)
        {
            _playerData = playerData;
        }

        public void SavePlayerData()
        {
            var data = JsonUtility.ToJson(_playerData);

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

            _playerData = JsonUtility.FromJson<PlayerData>(data);
            Debug.Log($"data: {data}");
        }
    }
}