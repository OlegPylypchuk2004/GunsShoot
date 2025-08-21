using SaveSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EditorTools
{
    public class EditorTool : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                SaveManager.Save();
            }
            else if (Input.GetKeyDown(KeyCode.I))
            {
                SaveManager.LogSavePath();
            }
            else if (Input.GetKeyDown(KeyCode.Delete))
            {
                SaveManager.Delete();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}