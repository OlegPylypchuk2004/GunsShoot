using Gameplay.States;
using Patterns.StateMachine;
using UnityEngine;
using VContainer;

namespace Gameplay
{
    public class GameplayManager : MonoBehaviour
    {
        private StateMachine _stateMachine;
        private PlayState _playState;

        [Inject]
        private void Construct(StateMachine stateMachine, PlayState playState)
        {
            _stateMachine = stateMachine;
            _playState = playState;
        }

        private void Start()
        {
            _stateMachine.ChangeState(_playState);
        }

        private void Update()
        {
            _stateMachine.CurrentState?.Update();
        }
    }
}