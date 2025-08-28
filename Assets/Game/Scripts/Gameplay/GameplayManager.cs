using GameModeSystem;
using Gameplay.States;
using Patterns.StateMachine;
using System;
using VContainer.Unity;

namespace Gameplay
{
    public class GameplayManager : IStartable, ITickable, IDisposable
    {
        private readonly IGameMode _gameMode;
        private readonly StateMachine _stateMachine;
        private readonly PreGameplayState _preGameplayState;
        private readonly GameplayState _gameplayState;
        private readonly GameOverState _gameOverState;

        public GameplayManager(IGameMode gameMode, StateMachine stateMachine, PreGameplayState preGameState, GameplayState playState, GameOverState gameOverState)
        {
            _gameMode = gameMode;
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
            _gameMode.GameOver -= OnGameOver;
        }

        private void OnGameReady()
        {
            _preGameplayState.GameReady -= OnGameReady;
            _stateMachine.ChangeState(_gameplayState);
            _gameMode.GameOver += OnGameOver;
        }

        private void OnGameOver(bool isCompleted)
        {
            _gameMode.GameOver -= OnGameOver;
            _stateMachine.ChangeState(_gameOverState);
        }
    }
}