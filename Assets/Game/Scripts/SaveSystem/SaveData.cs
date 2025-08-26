using BlasterSystem;
using System;
using System.Collections.Generic;

namespace SaveSystem
{
    [Serializable]
    public class SaveData
    {
        //Player data
        public Dictionary<string, int> Currencies;
        public Dictionary<string, int> Blasters;
        public Dictionary<string, int> GameModes;
        public string SelectedBlasterID;
        public bool IsFirstEntry;

        //Settings data
        public bool IsSoundEnabled;
        public bool IsMusicEnabled;
        public bool IsShowFPS;

        public SaveData()
        {
            Currencies = new Dictionary<string, int>();
            Blasters = new Dictionary<string, int>();
            GameModes = new Dictionary<string, int>();
            IsFirstEntry = true;
        }

        public bool IsBlasterPurchased(BlasterConfig blasterConfig)
        {
            return Blasters.ContainsKey(blasterConfig.ID);
        }
    }
}