using Cysharp.Threading.Tasks;
using Patterns.StateMachine;
using System;

namespace Gameplay.States
{
    public class PreGameplayState : State
    {
        public event Action GameReady;

        public override void Enter()
        {
            base.Enter();

            CountTime().Forget();
        }

        private async UniTaskVoid CountTime()
        {
            await UniTask.Delay(1000);

            GameReady?.Invoke();
        }
    }
}