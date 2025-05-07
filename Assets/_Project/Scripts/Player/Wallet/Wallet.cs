using UnityEngine.Events;

namespace _Project.Scripts.City.Wallet
{
    public class Wallet
    {
        private int _score;
        private int _capacity;
        
        public event UnityAction<int> ScoreChanged;
        public event UnityAction Fulled;
        
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
                Fulled?.Invoke();
            }
            
            ScoreChanged?.Invoke(_score);
        }
        
        public void ClearScore(int money)
        {
            _score = 0;
            ScoreChanged?.Invoke(_score);
        }
    }
}