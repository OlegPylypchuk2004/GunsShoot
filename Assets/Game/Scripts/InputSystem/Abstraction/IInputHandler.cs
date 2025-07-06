namespace InputSystem
{
    public interface IInputHandler
    {
        public bool IsAim { get; }
        public bool IsShoot { get; }
    }
}