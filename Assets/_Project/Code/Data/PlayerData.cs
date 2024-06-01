using System;
using System.Collections.Generic;

namespace Code
{
    public class PlayerData
    {
        public Dictionary<int, int> levelStates = new Dictionary<int, int>();

        [NonSerialized] public int currentId;
    }
}