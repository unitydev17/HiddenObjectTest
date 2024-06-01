using System.Collections.Generic;

namespace Code.Services
{
    public interface IPlayerService
    {
        public Dictionary<int, int> GetLevelStates();
        int GetLevelId();
        void SetLevelId(int id);
        void AddCounter(int id, int i);
        int GetCounter(int id);
        void IncreaseCounter(int id);
        void ResetProgress(int id);
        PlayerData GetData();
        void SetPlayerData(PlayerData data);
    }
}