using UnityEngine.Events;

namespace _Project.Scripts.City.Wallet
{
    public class Wallet
    {
        private int _score;
        private int _capacity;
        private bool _isFulled;

        public bool IsFulled => _isFulled;
        public event UnityAction<int> ScoreChanged;
        
        public Wallet(int capacity)
        {
            _capacity = capacity;
        }

        public void AddScore(int score)
        {
            _score += score;

            if (_score >= _capacity)
            {
                _score = _capacity;
                _isFulled = true;
            }
            
            ScoreChanged?.Invoke(_score);
        }
        
        public void ClearScore()
        {
            _score = 0;
            _isFulled = false;
            ScoreChanged?.Invoke(_score);
        }
    }
}