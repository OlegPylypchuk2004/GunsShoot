using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class PointerOverUICheck : MonoBehaviour
    {
        [SerializeField] private EventSystem _eventSystem;

        public bool IsPointerOverUIElement()
        {
            if (_eventSystem == null)
            {
                _eventSystem = EventSystem.current;

                if (_eventSystem == null)
                {
                    return false;
                }
            }

            if (Input.touchCount > 0)
            {
                foreach (Touch touch in Input.touches)
                {
                    if (_eventSystem.IsPointerOverGameObject(touch.fingerId))
                    {
                        return true;
                    }
                }
            }
            else
            {
                if (_eventSystem.IsPointerOverGameObject())
                {
                    return true;
                }
            }

            return false;
        }
    }
}