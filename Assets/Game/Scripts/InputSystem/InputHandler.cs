using UnityEngine;

namespace InputSystem
{
    public class InputHandler : IInputHandler
    {
        public bool IsAim
        {
            get
            {
                if (Input.touchCount > 0)
                {
                    foreach (Touch touch in Input.touches)
                    {
                        if (GetScreenPercentX(touch.position.x) >= 0.75f)
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
        }

        public bool IsShoot
        {
            get
            {
                if (!IsAim)
                {
                    return false;
                }

                if (Input.touchCount > 0)
                {
                    foreach (Touch touch in Input.touches)
                    {
                        if (GetScreenPercentX(touch.position.x) < 0.75f)
                        {
                            return true;
                        }
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