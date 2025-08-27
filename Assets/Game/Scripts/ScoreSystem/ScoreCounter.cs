using System;

namespace ScoreSystem
{
    public class ScoreCounter
    {
        private int _score;

        public event Action<int> ScoreChanged;

        public int Score
        {
            get
            {
                return _score;
            }
            private set
            {
                if (_score == value)
                {
                    return;
                }

                _score = value;

                ScoreChanged?.Invoke(_score);
            }
        }
    }
}