using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ToggleButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private RectTransform _pointerRectTransform;
        [SerializeField, Min(0f)] private float _pointerAnimationDuration;
        [SerializeField] private Ease _pointerAnimationEase;
        [SerializeField] private Vector2 _enabledPointerPosition;
        [SerializeField] private Vector2 _disabledPointerPosition;

        private bool _isEnabled;
        private Tween _currentTween;

        public event Action<bool> StateChanged;

        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                if (_isEnabled == value)
                {
                    return;
                }

                _isEnabled = value;

                StateChanged?.Invoke(_isEnabled);
            }
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            _currentTween?.Kill();

            _button.onClick.RemoveListener(OnButtonClicked);
        }

        public void Initialize(bool isEnabled)
        {
            _isEnabled = isEnabled;

            _currentTween?.Kill();

            _pointerRectTransform.anchoredPosition = GetPointerTargetAnchor();
        }

        private void OnButtonClicked()
        {
            if (_currentTween != null && _currentTween.IsActive())
            {
                return;
            }

            IsEnabled = !IsEnabled;

            PlayPointerAnimation();
        }

        private Tween PlayPointerAnimation()
        {
            _currentTween?.Kill();

            _currentTween = _pointerRectTransform.DOAnchorPos(GetPointerTargetAnchor(), _pointerAnimationDuration)
                .SetEase(_pointerAnimationEase)
                .SetLink(gameObject);

            return _currentTween;
        }

        private Vector2 GetPointerTargetAnchor()
        {
            return IsEnabled ? _enabledPointerPosition : _disabledPointerPosition;
        }
    }
}