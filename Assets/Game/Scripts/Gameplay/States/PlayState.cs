using BlasterSystem;
using Patterns.StateMachine;
using PauseManagment;
using UnityEngine;
using VContainer;

namespace Gameplay.States
{
    public class PlayState : State
    {
        private PauseHandler _pauseHandler;
        private BlasterController _blasterController;

        [Inject]
        private void Construct(PauseHandler pauseHandler, BlasterController blasterController)
        {
            _pauseHandler = pauseHandler;
            _blasterController = blasterController;
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

            if (Input.GetMouseButtonDown(1))
            {
                _blasterController.UpdateRotationOffset();
            }

            if (Input.GetMouseButton(1))
            {
                _blasterController.Rotate();
            }
        }
    }
}