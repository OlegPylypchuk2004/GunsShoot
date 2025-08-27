using UnityEngine;
using VContainer;

namespace ScoreSystem
{
    public class ScoreDisplay : MonoBehaviour
    {
        private ScoreCounter _scoreCounter;

        [Inject]
        private void Construct(ScoreCounter scoreCounter)
        {
            _scoreCounter = scoreCounter;
        }
    }
}