using DG.Tweening;
using Gameplay.States;
using InputSystem;
using System.Collections;
using UnityEngine;
using VContainer;

namespace TutorialSystem
{
    public class GameplayTutorial : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField, Min(0f)] private float _disappearDuration;
        [SerializeField] private Ease _disappearEase;
        [SerializeField] private Animator _handAnimator;

        private PreGameplayState _preGameplayState;
        private IInputHandler _inputHandler;
        private Coroutine _wainUntilAimCoroutine;
        private Tween _disappearTween;

        [Inject]
        private void Construct(PreGameplayState preGameplayState, IInputHandler inputHandler)
        {
            _preGameplayState = preGameplayState;
            _inputHandler = inputHandler;
        }

        private void Start()
        {
            _preGameplayState.GameReady += OnGameReady;

            _handAnimator.SetTrigger("Start");
        }

        private void OnDestroy()
        {
            _preGameplayState.GameReady -= OnGameReady;
        }

        private void OnGameReady()
        {
            _preGameplayState.GameReady -= OnGameReady;

            if (_wainUntilAimCoroutine == null)
            {
                _wainUntilAimCoroutine = StartCoroutine(WaitUntilAimCoroutine());
            }
        }

        private IEnumerator WaitUntilAimCoroutine()
        {
            yield return new WaitUntil(() => _inputHandler.IsAim);

            _handAnimator.SetTrigger("Stop");
            _handAnimator.gameObject.SetActive(false);

            Disappear();
        }

        private Tween Disappear()
        {
            _disappearTween?.Kill();

            _disappearTween = _canvasGroup.DOFade(0f, _disappearDuration)
                .SetEase(_disappearEase)
                .SetLink(gameObject);

            return _disappearTween;
        }
    }
}