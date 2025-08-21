using BlasterSystem;
using EditorTools;
using SaveSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Global
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private EditorTool _editorTool;
        [SerializeField] private BlasterConfig[] _initialBoughtBlasters;

        private void Start()
        {
            InitializeEditorTool();
            BuyInitialBlasters();

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        private void InitializeEditorTool()
        {
#if !UNITY_EDITOR
            Destroy(_editorTool.gameObject);
#endif
        }

        private void BuyInitialBlasters()
        {
            SaveData saveData = SaveManager.Data;

            foreach (BlasterConfig initialBoughtBlaster in _initialBoughtBlasters)
            {
                if (!saveData.IsBlasterPurchased(initialBoughtBlaster))
                {
                    BlasterData blasterData = new BlasterData(initialBoughtBlaster.ID, 1);
                    saveData.Blasters.Add(blasterData);
                    SaveManager.Save();
                }
            }
        }
    }
}