using DG.Tweening;
using UnityEngine;

namespace Effects
{
    public class PatternBackground : MonoBehaviour
    {
        [SerializeField] private GameObject[] _chunks;
        [SerializeField] private float _speed;

        private float _minYPosition;
        private float _maxYPosition;

        private void Awake()
        {
            float spacing = 0f;

            if (_chunks.Length > 1)
            {
                spacing = _chunks[1].transform.localPosition.y - _chunks[0].transform.localPosition.y;
            }

            _minYPosition = _chunks[_chunks.Length - 1].transform.localPosition.y + spacing;
            _maxYPosition = _chunks[0].transform.localPosition.y;
        }

        private void Start()
        {
            foreach (GameObject chunk in _chunks)
            {
                MoveChunk(chunk);
            }
        }

        private Tween MoveChunk(GameObject chunk)
        {
            return chunk.transform.DOLocalMoveY(_minYPosition, _speed)
                .SetEase(Ease.Linear)
                .SetSpeedBased()
                .SetLink(gameObject)
                .OnComplete(() =>
                {
                    chunk.transform.localPosition = new Vector2(chunk.transform.localPosition.x, _maxYPosition);

                    MoveChunk(chunk);
                });
        }
    }
}