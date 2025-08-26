using BlasterSystem;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SaveSystem
{
    [Serializable]
    public class SaveData
    {
        //Player data
        public Dictionary<string, int> Currencies;
        public List<BlasterData> Blasters;
        public string SelectedBlasterID;
        public bool IsFirstEntry;

        //Settings data
        public bool IsSoundEnabled;
        public bool IsMusicEnabled;
        public bool IsShowFPS;

        public SaveData()
        {
            Currencies = new Dictionary<string, int>();
            Blasters = new List<BlasterData>();
            IsFirstEntry = true;
        }

        public bool IsBlasterPurchased(BlasterConfig blasterConfig)
        {
            return Blasters.Any(blaster => blaster.ID == blasterConfig.ID);
        }
    }
}