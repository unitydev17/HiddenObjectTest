using System;
using System.Collections.Generic;

namespace Code
{
    [Serializable]
    public class RemoteConfig
    {
        public List<Level> levels;

        public RemoteConfig()
        {
            levels = new List<Level>();
        }

        public void Add(Level level)
        {
            levels.Add(level);
        }
    }
}