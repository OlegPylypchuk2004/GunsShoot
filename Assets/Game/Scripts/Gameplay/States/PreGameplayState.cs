using Patterns.StateMachine;
using System;
using UnityEngine;

namespace Gameplay.States
{
    public class PreGameplayState : State
    {
        public event Action GameReady;

        public override void Update()
        {
            base.Update();

            if (Input.GetMouseButtonDown(1))
            {
                GameReady?.Invoke();
            }
        }
    }
}