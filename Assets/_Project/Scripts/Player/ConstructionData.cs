using UnityEngine.Events;

namespace _Project.Scripts.City
{
    public class ConstructionData
    {
        private int _initHealth;
        private int _health;
        private int _initEarn;
        private int _earn;
        
        public int Earn => _earn;
        public int InitHealth => _initHealth;
        public int Health => _health;
        public event UnityAction HealthChanged; 

        public void SetData(int health, int earn)
        {
            _initHealth = health;
            _health = _initHealth;
            _initEarn = earn;
            _earn = _initEarn;
            HealthChanged?.Invoke();
        }
        
        public void TakeDamage(int damage)
        {
            _health -= damage;

            if (_health < 0)
            {
                _health = 0;
                _earn = 0;
            }
            
            HealthChanged?.Invoke();
        }
    }
}
