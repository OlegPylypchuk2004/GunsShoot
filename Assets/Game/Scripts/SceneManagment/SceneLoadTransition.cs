using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VContainer;

namespace SceneManagment
{
    public class SceneLoadTransition : MonoBehaviour
    {
        [SerializeField, Range(0f, 1f)] private float _maxBackgroundAlpha;
        [SerializeField, Range(0f, 1f)] private float _backgroundTransitionDuration;
        [SerializeField, Min(0f)] private float _appearDelay;
        [SerializeField] private EventSystem _eventSystem;
        [SerializeField] private Image _background;

        private SceneLoader _sceneLoader;
        private Sequence _currentSequence;

        [Inject]
        private void Construct(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private void Start()
        {
            Disappear();
        }

        private void OnEnable()
        {
            _sceneLoader.LoadStarted += OnSceneLoadStarted;
        }

        private void OnDisable()
        {
            _sceneLoader.LoadStarted -= OnSceneLoadStarted;
        }

        private void OnSceneLoadStarted()
        {
            Appear();
        }

        private Sequence Appear()
        {
            _currentSequence?.Kill();

            _currentSequence = DOTween.Sequence();
            _currentSequence.SetLink(gameObject);

            _currentSequence.AppendCallback(() =>
            {
                if (_eventSystem != null)
                {
                    _eventSystem.gameObject.SetActive(false);
                }

                _background.gameObject.SetActive(true);
            });

            _currentSequence.AppendInterval(_appearDelay);

            _currentSequence.Append(_background.DOFade(_maxBackgroundAlpha, _backgroundTransitionDuration)
                .From(0f)
                .SetEase(Ease.OutQuad));

            return _currentSequence;
        }

        private Sequence Disappear()
        {
            _currentSequence?.Kill();

            _currentSequence = DOTween.Sequence();
            _currentSequence.SetLink(gameObject);

            _currentSequence.AppendCallback(() =>
            {
                if (_eventSystem != null)
                {
                    _eventSystem.gameObject.SetActive(false);
                }

                _background.gameObject.SetActive(true);
            });

            _currentSequence.Append(_background.DOFade(0f, _backgroundTransitionDuration)
                .SetEase(Ease.InQuad));

            _currentSequence.AppendCallback(() =>
            {
                if (_eventSystem != null)
                {
                    _eventSystem.gameObject.SetActive(true);
                }

                _background.gameObject.SetActive(false);
            });

            return _currentSequence;
        }
    }
}