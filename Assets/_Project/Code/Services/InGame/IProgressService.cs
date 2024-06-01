namespace Code.Services
{
    public interface IProgressService
    {
        public void IncreaseCounter(Level level);
        public (int, bool) GetProgress(Level level);
        public void ResetProgress(Level level);
    }
}