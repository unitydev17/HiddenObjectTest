namespace Code.Services
{
    public class ProgressService : IProgressService
    {
        private readonly IPlayerService _playerService;

        public ProgressService(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        private int GetCounter(Level level)
        {
            var id = level.id;
            if (!_playerService.GetLevelStates().ContainsKey(id)) _playerService.AddCounter(id, 0);

            return _playerService.GetCounter(id);
        }

        public void IncreaseCounter(Level level)
        {
            _playerService.IncreaseCounter(level.id);
        }

        public (int, bool) GetProgress(Level level)
        {
            var progress = level.counter - GetCounter(level);
            return (progress, progress == 0);
        }

        public void ResetProgress(Level level)
        {
            _playerService.ResetProgress(level.id);
        }
    }
}