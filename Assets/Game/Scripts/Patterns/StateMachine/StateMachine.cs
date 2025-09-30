namespace Patterns.StateMachine
{
    public class StateMachine
    {
        public State CurrentState { get; private set; }

        public void ChangeState(State state)
        {
            if (CurrentState != null)
            {
                CurrentState.Exit();
            }

            CurrentState = state;
            CurrentState.Enter();
        }
    }
}