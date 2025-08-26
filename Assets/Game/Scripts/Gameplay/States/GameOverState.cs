using Gameplay.UI;
using Global;
using Patterns.StateMachine;
using SaveSystem;

namespace Gameplay.States
{
    public class GameOverState : State
    {
        private GameOverPanel _gameOverDisplay;

        public GameOverState(GameOverPanel gameOverDisplay)
        {
            _gameOverDisplay = gameOverDisplay;
        }

        public override void Enter()
        {
            base.Enter();

            SaveData();
            _gameOverDisplay.Appear();
        }

        private void SaveData()
        {
            string gameModeID = LocalGameData.GameModeConfig.ID;
            SaveData saveData = SaveManager.Data;

            if (saveData.GameModes.ContainsKey(gameModeID))
            {
                saveData.GameModes[gameModeID] = 100;
            }
            else
            {
                saveData.GameModes.Add(gameModeID, 50);
            }

            SaveManager.Save();
        }
    }
}