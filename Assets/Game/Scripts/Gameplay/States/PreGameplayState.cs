using Cysharp.Threading.Tasks;
using Patterns.StateMachine;
using System;
using UnityEngine;

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
            Debug.Log("3");
            await UniTask.Delay(1000);
            Debug.Log("2");
            await UniTask.Delay(1000);
            Debug.Log("1");

            GameReady?.Invoke();
        }
    }
}