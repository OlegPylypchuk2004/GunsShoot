using BlasterSystem;
using Cysharp.Threading.Tasks;
using Patterns.StateMachine;
using System;

namespace Gameplay.States
{
    public class PreGameplayState : State
    {
        private BlasterHolder _blasterHolder;

        public event Action GameReady;

        public PreGameplayState(BlasterHolder blasterHolder)
        {
            _blasterHolder = blasterHolder;
        }

        public override void Enter()
        {
            base.Enter();

            CountTime().Forget();
        }

        private async UniTaskVoid CountTime()
        {
            await UniTask.Delay(500);

            _blasterHolder.ChangeBlasterRandom();

            await UniTask.Delay(500);

            GameReady?.Invoke();
        }
    }
}