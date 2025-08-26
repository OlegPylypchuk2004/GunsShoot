using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Toggle : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private RectTransform _pointerRectTransform;
        [SerializeField, Min(0f)] private float _pointerAnimationDuration;
        [SerializeField] private Ease _pointerAnimationEase;

        private bool _isEnabled;
        private float _initialPointerPositionX;
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

        private void Awake()
        {
            _initialPointerPositionX = _pointerRectTransform.anchoredPosition.x;
        }

        private void OnEnable()
        {
            _currentTween?.Kill();

            _pointerRectTransform.anchoredPosition = new Vector2(GetPointerTargetPositionX(), _pointerRectTransform.anchoredPosition.y);

            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            _currentTween?.Kill();

            _button.onClick.RemoveListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            if (_currentTween != null && _currentTween.IsPlaying())
            {
                return;
            }

            IsEnabled = !IsEnabled;

            PlayPointerAnimation();
        }

        private Tween PlayPointerAnimation()
        {
            _currentTween?.Kill();

            _currentTween = _pointerRectTransform.DOAnchorPosX(GetPointerTargetPositionX(), _pointerAnimationDuration)
                .SetEase(_pointerAnimationEase)
                .SetLink(gameObject);

            return _currentTween;
        }

        private float GetPointerTargetPositionX()
        {
            if (IsEnabled)
            {
                return _initialPointerPositionX;
            }
            else
            {
                return -_initialPointerPositionX;
            }
        }
    }
}