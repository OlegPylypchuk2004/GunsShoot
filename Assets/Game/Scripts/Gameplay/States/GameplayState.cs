using BlasterSystem;
using HealthSystem;
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
        private BlasterHolder _blasterHolder;
        private BlasterController _blasterController;
        private ObstacleSpawner _obstacleSpawner;
        private HealthManager _healthManager;

        public event Action GameOver;

        [Inject]
        private void Construct(PauseHandler pauseHandler, BlasterHolder blasterHolder, BlasterController blasterController, ObstacleSpawner obstacleSpawner, HealthManager healthManager)
        {
            _pauseHandler = pauseHandler;
            _blasterHolder = blasterHolder;
            _blasterController = blasterController;
            _obstacleSpawner = obstacleSpawner;
            _healthManager = healthManager;
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

            if (Input.GetMouseButton(1))
            {
                _pauseHandler.IsPaused = false;

                if (Input.GetMouseButton(0))
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

        private void OnHealthIsOver()
        {
            GameOver?.Invoke();
        }
    }
}