using Gameplay.States;
using Patterns.StateMachine;
using System;
using VContainer.Unity;

namespace Gameplay
{
    public class GameplayManager : IStartable, ITickable, IDisposable
    {
        private readonly StateMachine _stateMachine;
        private readonly PreGameplayState _preGameplayState;
        private readonly GameplayState _gameplayState;
        private readonly GameOverState _gameOverState;

        public GameplayManager(StateMachine stateMachine, PreGameplayState preGameState, GameplayState playState, GameOverState gameOverState)
        {
            _stateMachine = stateMachine;
            _preGameplayState = preGameState;
            _gameplayState = playState;
            _gameOverState = gameOverState;
        }

        public void Start()
        {
            _preGameplayState.GameReady += OnGameReady;
            _stateMachine.ChangeState(_preGameplayState);
        }

        public void Tick()
        {
            _stateMachine.CurrentState?.Update();
        }

        public void Dispose()
        {
            _preGameplayState.GameReady -= OnGameReady;
            _gameplayState.GameOver -= OnGameOver;
        }

        private void OnGameReady()
        {
            _preGameplayState.GameReady -= OnGameReady;
            _stateMachine.ChangeState(_gameplayState);
            _gameplayState.GameOver += OnGameOver;
        }

        private void OnGameOver()
        {
            _gameplayState.GameOver -= OnGameOver;
            _stateMachine.ChangeState(_gameOverState);
        }
    }
}