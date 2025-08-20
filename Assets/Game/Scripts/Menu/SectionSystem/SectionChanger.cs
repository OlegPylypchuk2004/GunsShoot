using DG.Tweening;
using System;
using UnityEngine;

namespace Menu.SectionSystem
{
    public class SectionChanger : MonoBehaviour
    {
        [SerializeField] private Section _defaultSection;

        private Section _currentSection;

        public event Action<Section> Changed;

        private void Awake()
        {
            _currentSection = _defaultSection;
            _currentSection.Activate();
        }

        public void Change(Section section)
        {
            if (_currentSection == section)
            {
                return;
            }

            Section previousSection = _currentSection;
            _currentSection = section;

            Changed?.Invoke(_currentSection);

            previousSection.DisappearUI()
                .OnKill(() =>
                {
                    _currentSection.Activate();

                    _currentSection.AppearUI()
                        .OnKill(() =>
                        {
                            previousSection.Deactivate();
                        });
                });
        }
    }
}