using UnityEngine;

namespace InputSystem
{
    public class MobileInputHandler : IInputHandler
    {
        public bool IsAim
        {
            get
            {
                foreach (Touch touch in Input.touches)
                {
                    if (GetScreenPercentX(touch.position.x) >= 0.65f)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public bool IsShoot
        {
            get
            {
                foreach (Touch touch in Input.touches)
                {
                    if (GetScreenPercentX(touch.position.x) < 0.35f)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        private float GetScreenPercentX(float pixelPositionX)
        {
            return pixelPositionX / Screen.width;
        }
    }
}