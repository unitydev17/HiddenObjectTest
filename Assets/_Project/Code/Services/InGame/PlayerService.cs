using System.Collections.Generic;

namespace Code.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly PlayerData _playerData;

        public PlayerService(PlayerData playerData)
        {
            _playerData = playerData;
        }

        public Dictionary<int, int> GetLevelStates()
        {
            return _playerData.levelStates;
        }

        public int GetLevelId()
        {
            return _playerData.levelId;
        }

        public void SetLevelId(int id)
        {
            _playerData.levelId = id;
        }

        public void AddCounter(int id, int value)
        {
            _playerData.levelStates.Add(id, value);
        }

        public int GetCounter(int id)
        {
            return _playerData.levelStates[id];
        }

        public void IncreaseCounter(int id)
        {
            _playerData.levelStates[id]++;
        }

        public void ResetProgress(int id)
        {
            _playerData.levelStates[id] = 0;
        }

        public PlayerData GetData()
        {
            return _playerData;
        }

        public void SetPlayerData(PlayerData data)
        {
            _playerData.levelStates = data.levelStates;
            _playerData.levelId = data.levelId;
        }
    }
}