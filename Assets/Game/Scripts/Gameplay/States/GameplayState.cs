using BlasterSystem;
using ComboSystem;
using HealthSystem;
using InputSystem;
using ObstacleSystem;
using Patterns.StateMachine;
using PauseManagment;
using System;
using UnityEngine;
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
        private HealthManager _healthManager;
        private ComboCounter _comboCounter;

        public event Action GameOver;

        [Inject]
        private void Construct(PauseHandler pauseHandler, IInputHandler inputHandler, BlasterHolder blasterHolder, BlasterController blasterController, ObstacleSpawner obstacleSpawner, HealthManager healthManager, ComboCounter comboCounter)
        {
            _pauseHandler = pauseHandler;
            _inputHandler = inputHandler;
            _blasterHolder = blasterHolder;
            _blasterController = blasterController;
            _obstacleSpawner = obstacleSpawner;
            _healthManager = healthManager;
            _comboCounter = comboCounter;
        }

        public override void Enter()
        {
            base.Enter();

            _obstacleSpawner.Activate();
            _healthManager.HealthIsOver += OnHealthIsOver;
        }

        public override void Exit()
        {
            base.Exit();

            _obstacleSpawner.Deactivate();
            _healthManager.HealthIsOver -= OnHealthIsOver;
        }

        public override void Update()
        {
            base.Update();

            if (_inputHandler.IsAim)
            {
                _pauseHandler.IsPaused = false;

                if (_inputHandler.IsShoot)
                {
                    _blasterHolder.Blaster?.Shoot();
                }

                _comboCounter.Update();
            }
            else
            {
                _pauseHandler.IsPaused = true;
            }

            _blasterController.UpdateRotation();
        }

        private void OnHealthIsOver()
        {
            GameOver?.Invoke();
        }
    }
}