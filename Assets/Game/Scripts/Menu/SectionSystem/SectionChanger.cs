using DG.Tweening;
using System;
using UnityEngine;

namespace Menu.SectionSystem
{
    public class SectionChanger : MonoBehaviour
    {
        [SerializeField] private MainSection _mainSection;
        [SerializeField] private ShopSection _shopSection;

        private Section _currentSection;

        public event Action<Section> Changed;

        private void Awake()
        {
            _currentSection = _mainSection;
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
            if (_currentSection == section)
            {
                return;
            }

            Section previousSection = _currentSection;
            _currentSection = section;

            Changed?.Invoke(_currentSection);

            previousSection.Disappear()
                .OnKill(() =>
                {
                    _currentSection.Appear();
                });
        }
    }
}