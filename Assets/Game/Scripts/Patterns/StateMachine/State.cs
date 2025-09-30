using UnityEngine;

namespace Patterns.StateMachine
{
    public abstract class State
    {
        public virtual void Enter()
        {
            Debug.Log($"State changed: {this}");
        }

        public virtual void Exit()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void FixedUpdate()
        {

        }

        public virtual void LateUpdate()
        {

        }
    }
}