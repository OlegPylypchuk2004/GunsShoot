using BlasterSystem;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public bool IsBlasterPurchased(BlasterConfig blasterConfig)
        {
            return Blasters.Any(blaster => blaster.ID == blasterConfig.ID);
        }
    }
}