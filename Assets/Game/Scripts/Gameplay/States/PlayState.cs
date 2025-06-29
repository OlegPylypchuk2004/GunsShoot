using Patterns.StateMachine;
using PauseManagment;
using UnityEngine;
using VContainer;

namespace Gameplay.States
{
    public class PlayState : State
    {
        private PauseHandler _pauseHandler;

        [Inject]
        private void Construct(PauseHandler pauseHandler)
        {
            _pauseHandler = pauseHandler;
        }

        public override void Update()
        {
            base.Update();

            if (Input.GetMouseButton(1))
            {
                _pauseHandler.IsPaused = false;
            }
            else
            {
                _pauseHandler.IsPaused = true;
            }
        }
    }
}