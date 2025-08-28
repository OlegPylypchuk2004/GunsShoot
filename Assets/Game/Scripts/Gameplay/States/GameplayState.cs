using BlasterSystem;
using ComboSystem;
using InputSystem;
using ObstacleSystem;
using Patterns.StateMachine;
using PauseManagment;
using VContainer;

namespace Gameplay.States
{
    public class GameplayState : State
    {
        private PauseHandler _pauseHandler;
        private IInputHandler _inputHandler;
        private BlasterHolder _blasterHolder;
        private BlasterController _blasterController;
        private ObstacleSpawner _obstacleSpawner;
        private ComboCounter _comboCounter;

        [Inject]
        private void Construct(PauseHandler pauseHandler, IInputHandler inputHandler, BlasterHolder blasterHolder, BlasterController blasterController, ObstacleSpawner obstacleSpawner, ComboCounter comboCounter)
        {
            _pauseHandler = pauseHandler;
            _inputHandler = inputHandler;
            _blasterHolder = blasterHolder;
            _blasterController = blasterController;
            _obstacleSpawner = obstacleSpawner;
            _comboCounter = comboCounter;
        }

        public override void Enter()
        {
            base.Enter();

            _obstacleSpawner.Activate();
        }

        public override void Exit()
        {
            base.Exit();

            _obstacleSpawner.Deactivate();
        }

        public override void Update()
        {
            base.Update();

            if (_inputHandler.IsAim)
            {
                _pauseHandler.IsPaused = false;
                _comboCounter.Update();

                if (_inputHandler.IsShoot)
                {
                    _blasterHolder.Blaster?.Shoot();
                }
            }
            else
            {
                _pauseHandler.IsPaused = true;
            }

            _blasterController.UpdateRotation();
        }
    }
}