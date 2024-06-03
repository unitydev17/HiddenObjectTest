using System.Collections.Generic;

namespace Code.Services
{
    public interface ILevelService
    {
        public Level GetCurrLevel();
        public void SetCurrentLevel(int id);

        public List<Level> GetLevels();
    }
}