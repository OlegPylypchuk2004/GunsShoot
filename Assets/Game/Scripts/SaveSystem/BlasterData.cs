using System;

namespace SaveSystem
{
    [Serializable]
    public class BlasterData
    {
        public string ID;
        public int Level;

        public BlasterData(string id, int level)
        {
            ID = id;
            Level = level;
        }
    }
}