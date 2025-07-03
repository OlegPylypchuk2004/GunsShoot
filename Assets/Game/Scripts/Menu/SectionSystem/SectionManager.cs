using UnityEngine;

namespace Menu.SectionSystem
{
    public class SectionManager : MonoBehaviour
    {
        [SerializeField] private MainSection _mainSection;
        [SerializeField] private ShopSection _shopSection;

        private Section _currentSetion;

        private void Awake()
        {
            _currentSetion = _mainSection;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ChangedSection(_mainSection);
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                ChangedSection(_shopSection);
            }
        }

        private void ChangedSection(Section section)
        {
            if (_currentSetion == section)
            {
                return;
            }

            _currentSetion.Disappear();
            _currentSetion = section;
            _currentSetion.Appear();
        }
    }
}