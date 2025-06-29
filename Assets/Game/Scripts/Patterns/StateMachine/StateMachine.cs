namespace Patterns.StateMachine
{
    public class StateMachine
    {
        public StateMachine(State initialState = null)
        {
            if (initialState != null)
            {
                ChangeState(initialState);
            }
        }

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