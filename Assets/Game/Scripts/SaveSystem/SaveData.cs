using System;
using System.Collections.Generic;

namespace SaveSystem
{
    [Serializable]
    public class SaveData
    {
        public List<BlasterData> Blasters;

        public SaveData()
        {
            Blasters = new List<BlasterData>();
        }
    }
}