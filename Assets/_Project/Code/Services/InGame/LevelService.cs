using System.Collections.Generic;
using System.Linq;

namespace Code.Services
{
    public class LevelService : ILevelService
    {
        private readonly GameData _gameData;
        private readonly IPlayerDataService _playerDataService;

        public LevelService(GameData gameData, IPlayerDataService playerDataService)
        {
            _gameData = gameData;
            _playerDataService = playerDataService;
        }

        public Level GetCurrLevel()
        {
            return _gameData.remoteConfig.levels.First(level => level.id == _playerDataService.GetLevelId());
        }

        public void SetCurrentLevel(int id)
        {
            _playerDataService.SetLevelId(id);
        }

        public IEnumerable<Level> GetLevels()
        {
            return _gameData.remoteConfig.levels;
        }
    }
}