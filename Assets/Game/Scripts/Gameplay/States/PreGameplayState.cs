using BlasterSystem;
using Cysharp.Threading.Tasks;
using InputSystem;
using Patterns.StateMachine;
using StageSystem;
using System;

namespace Gameplay.States
{
    public class PreGameplayState : State
    {
        private BlasterHolder _blasterHolder;
        private StageManager _stageManager;
        private IInputHandler _inputHandler;

        public event Action GameReady;

        public PreGameplayState(BlasterHolder blasterHolder, StageManager stageManager, IInputHandler inputHandler)
        {
            _blasterHolder = blasterHolder;
            _stageManager = stageManager;
            _inputHandler = inputHandler;
        }

        public override async void Enter()
        {
            base.Enter();

            await _stageManager.LoadStages();

            CountTime().Forget();
        }

        private async UniTaskVoid CountTime()
        {
            await UniTask.Delay(500);

            _blasterHolder.SpawnBlaster();

            await UniTask.Delay(500);

            await UniTask.WaitUntil(() => _inputHandler.IsAim);

            GameReady?.Invoke();
        }
    }
}