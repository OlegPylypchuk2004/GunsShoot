using BlasterSystem;
using SaveSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Global
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private BlasterConfig[] _initialBoughtBlasters;
        [SerializeField] private BlasterConfig _initialSelectedBlaster;

        private void Start()
        {
            BuyInitialBlasters();
            SetInitialSelectedBlaster();

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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

        private void SetInitialSelectedBlaster()
        {
            SaveData saveData = SaveManager.Data;

            if (string.IsNullOrEmpty(saveData.SelectedBlasterID))
            {
                saveData.SelectedBlasterID = _initialSelectedBlaster.ID;
                SaveManager.Save();
            }
        }
    }
}