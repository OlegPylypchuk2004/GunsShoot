using BlasterSystem;
using SaveSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Global
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private BlasterConfig[] _initialBoughtBlasters;

        private void Start()
        {
            BuyInitialBlasters();

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
    }
}