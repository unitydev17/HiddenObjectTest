using System.Collections.Generic;
using System.Linq;

namespace Code.Services
{
    public class LevelService : ILevelService
    {
        private readonly GameData _gameData;
        private readonly IPlayerService _playerService;

        public LevelService(GameData gameData, IPlayerService playerService)
        {
            _gameData = gameData;
            _playerService = playerService;
        }

        public Level GetCurrLevel()
        {
            return _gameData.remoteConfig.levels.First(level => level.id == _playerService.GetLevelId());
        }

        public void SetCurrentLevel(int id)
        {
            _playerService.SetLevelId(id);
        }

        public IEnumerable<Level> GetLevels()
        {
            return _gameData.remoteConfig.levels;
        }
    }
}