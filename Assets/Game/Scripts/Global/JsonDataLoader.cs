using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace Global
{
    public class JsonDataLoader
    {
        public async UniTask<T> LoadJsonDataAsync<T>(string fileName)
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

            try
            {
                if (filePath.Contains("://") || filePath.Contains(":///"))
                {
                    var unityWebRequest = await UnityWebRequest.Get(filePath).SendWebRequest();

                    if (unityWebRequest.result != UnityWebRequest.Result.Success)
                    {
                        Debug.LogError($"Error loading file from StreamingAssets: {fileName}\n{unityWebRequest.error}");

                        return default;
                    }

                    return JsonConvert.DeserializeObject<T>(unityWebRequest.downloadHandler.text);
                }
                else
                {
                    if (!File.Exists(filePath))
                    {
                        Debug.LogError($"File not found at path: {filePath}");

                        return default;
                    }

                    return JsonConvert.DeserializeObject<T>(File.ReadAllText(filePath));
                }
            }
            catch (System.Exception exception)
            {
                Debug.LogError($"Error loading or deserializing JSON from file: {fileName}\n{exception.Message}");

                return default;
            }
        }
    }
}