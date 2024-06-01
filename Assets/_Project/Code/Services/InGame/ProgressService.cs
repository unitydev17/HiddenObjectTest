namespace Code.Services
{
    public class ProgressService : IProgressService
    {
        private readonly IPlayerDataService _playerDataService;

        public ProgressService(IPlayerDataService playerDataService)
        {
            _playerDataService = playerDataService;
        }

        public int GetCounter(Level level)
        {
            var id = level.id;
            if (!_playerDataService.GetLevelStates().ContainsKey(id)) _playerDataService.AddCounter(id, 0);

            return _playerDataService.GetCounter(id);
        }

        public void IncreaseCounter(Level level)
        {
            _playerDataService.IncreaseCounter(level.id);
        }

        public (int, bool) GetProgress(Level level)
        {
            var progress = level.counter - GetCounter(level);
            return (progress, progress == 0);
        }

        public void ResetProgress(Level level)
        {
            _playerDataService.ResetProgress(level.id);
        }
    }
}