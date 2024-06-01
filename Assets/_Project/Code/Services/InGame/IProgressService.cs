namespace Code.Services
{
    public interface IProgressService
    {
        public int GetCounter(Level level);
        public void IncreaseCounter(Level level);

        public (int, bool) GetProgress(Level level);
        public void ResetProgress(Level level);
    }
}