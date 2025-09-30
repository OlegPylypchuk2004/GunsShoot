using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public class AmmoDisplay : MonoBehaviour
    {
        [SerializeField] private Image _iconImage;

        public void Activate()
        {
            _iconImage.gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            _iconImage.gameObject.SetActive(false);
        }
    }
}