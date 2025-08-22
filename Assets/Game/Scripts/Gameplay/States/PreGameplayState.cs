using BlasterSystem;
using Cysharp.Threading.Tasks;
using Patterns.StateMachine;
using StageSystem;
using System;

namespace Gameplay.States
{
    public class PreGameplayState : State
    {
        private BlasterHolder _blasterHolder;
        private StageManager _stageManager;

        public event Action GameReady;

        public PreGameplayState(BlasterHolder blasterHolder, StageManager stageManager)
        {
            _blasterHolder = blasterHolder;
            _stageManager = stageManager;
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

            GameReady?.Invoke();
        }
    }
}