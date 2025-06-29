using Gameplay.States;
using Patterns.StateMachine;
using System;
using UnityEngine;
using VContainer.Unity;

namespace Gameplay
{
    public class GameplayManager : IStartable, ITickable, IDisposable
    {
        private readonly StateMachine _stateMachine;
        private readonly PreGameplayState _preGameplayState;
        private readonly GameplayState _gameplayState;

        public GameplayManager(StateMachine stateMachine, PreGameplayState preGameState, GameplayState playState)
        {
            _stateMachine = stateMachine;
            _preGameplayState = preGameState;
            _gameplayState = playState;
        }

        public void Start()
        {
            _stateMachine.ChangeState(_preGameplayState);
            _preGameplayState.GameReady += OnGameReady;
        }

        public void Tick()
        {
            _stateMachine.CurrentState?.Update();
        }

        public void Dispose()
        {
            Debug.Log("Disposed");
        }

        private void OnGameReady()
        {
            _stateMachine.ChangeState(_gameplayState);
            _preGameplayState.GameReady -= OnGameReady;
        }
    }
}