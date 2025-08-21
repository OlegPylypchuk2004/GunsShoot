using System.IO;
using UnityEngine;

namespace SaveSystem
{
    public static class SaveManager
    {
        private static string FilePath => Path.Combine(Application.persistentDataPath, "SaveData.json");

        private static SaveData _cachedData;

        public static SaveData Data
        {
            get
            {
                if (_cachedData == null)
                {
                    Load();
                }

                return _cachedData;
            }
        }

        public static void Save()
        {
            string json = JsonUtility.ToJson(_cachedData, true);
            File.WriteAllText(FilePath, json);
        }

        public static void Load()
        {
            if (File.Exists(FilePath))
            {
                string json = File.ReadAllText(FilePath);
                _cachedData = JsonUtility.FromJson<SaveData>(json);
            }
            else
            {
                _cachedData = new SaveData();
                Save();
            }
        }
    }
}