using UnityEngine;

namespace InputSystem
{
    public interface IInputHandler
    {
        public Vector2 AimPointerPosition { get; }
        public bool IsAim { get; }
        public bool IsShoot { get; }
    }
}