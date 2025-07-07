using UnityEngine;

namespace InputSystem
{
    public class MouseInputHandler : IInputHandler
    {
        public bool IsAim
        {
            get
            {
                return Input.GetMouseButton(1);
            }
        }

        public bool IsShoot
        {
            get
            {
                return Input.GetMouseButton(0);
            }
        }
    }
}