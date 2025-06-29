using PauseManagment;
using UnityEngine;
using VContainer;

namespace Gameplay
{
    public class GameplayManager : MonoBehaviour
    {
        private PauseHandler _pauseHandler;

        [Inject]
        private void Construct(PauseHandler pauseHandler)
        {
            _pauseHandler = pauseHandler;
        }

        private void Update()
        {
            _pauseHandler.Update();
        }
    }
}