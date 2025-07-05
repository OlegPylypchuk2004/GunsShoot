using DG.Tweening;
using UnityEngine;
using VContainer;

namespace Gameplay.UI
{
    public class GameOverDisplay : MonoBehaviour
    {
        private BlurBackground _blurBackground;

        [Inject]
        private void Construct(BlurBackground blurBackground)
        {
            _blurBackground = blurBackground;
        }

        public Sequence Appear()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.SetLink(gameObject);

            sequence.Append(_blurBackground.Appear());

            return sequence;
        }
    }
}