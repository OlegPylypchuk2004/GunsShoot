using DG.Tweening;
using PauseManagment;
using UnityEngine;
using VContainer;

namespace Gameplay.UI
{
    public class PausePanel : Panel
    {
        [SerializeField] private RectTransform _inputZonesParentRectTransform;
        [SerializeField] private RectTransform _shootInputZoneRectTransform;
        [SerializeField] private RectTransform _aimInputZoneRectTransform;

        private PauseHandler _pauseHandler;

        [Inject]
        private void Construct(PauseHandler pauseHandler)
        {
            _pauseHandler = pauseHandler;
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            UpdateInputZonesSize();

            _pauseHandler.Paused += OnPaused;
            _pauseHandler.Unpaused += OnUnpaused;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _pauseHandler.Paused -= OnPaused;
            _pauseHandler.Unpaused -= OnUnpaused;
        }

        private void LateUpdate()
        {
            UpdateInputZonesSize();
        }

        private void UpdateInputZonesSize()
        {
            float inputZonesParentRectTransformWidth = _inputZonesParentRectTransform.rect.size.x;
            _shootInputZoneRectTransform.sizeDelta = new Vector2(inputZonesParentRectTransformWidth * 0.65f - 62.5f, _shootInputZoneRectTransform.sizeDelta.y);
            _aimInputZoneRectTransform.sizeDelta = new Vector2(inputZonesParentRectTransformWidth * 0.35f - 62.5f, _aimInputZoneRectTransform.sizeDelta.y);
        }

        private void OnPaused()
        {
            Appear()
                .SetUpdate(true);
        }

        private void OnUnpaused()
        {
            Disappear()
                .SetUpdate(true);
        }
    }
}