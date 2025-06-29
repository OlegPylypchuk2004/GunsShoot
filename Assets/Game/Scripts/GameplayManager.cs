using Gameplay.States;
using Patterns.StateMachine;
using VContainer.Unity;

namespace Gameplay
{
    public class GameplayManager : IStartable, ITickable
    {
        private readonly StateMachine _stateMachine;
        private readonly PreGameState _preGameState;
        private readonly PlayState _playState;

        public GameplayManager(StateMachine stateMachine, PreGameState preGameState, PlayState playState)
        {
            _stateMachine = stateMachine;
            _preGameState = preGameState;
            _playState = playState;
        }

        public void Start()
        {
            _stateMachine.ChangeState(_preGameState);
        }

        public void Tick()
        {
            _stateMachine.CurrentState?.Update();
        }
    }
}