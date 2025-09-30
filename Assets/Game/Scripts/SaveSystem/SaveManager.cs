using System.IO;
using UnityEngine;
using Newtonsoft.Json;

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
            string json = JsonConvert.SerializeObject(_cachedData, Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }

        public static void Load()
        {
            if (File.Exists(FilePath))
            {
                string json = File.ReadAllText(FilePath);
                _cachedData = JsonConvert.DeserializeObject<SaveData>(json);

                if (_cachedData == null)
                {
                    _cachedData = new SaveData();
                    Save();
                }
            }
            else
            {
                _cachedData = new SaveData();
                Save();
            }
        }

        public static void Delete()
        {
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }

            _cachedData = new SaveData();
        }

        public static void LogSavePath()
        {
            Debug.Log($"Save file path: {FilePath}");
        }
    }
}