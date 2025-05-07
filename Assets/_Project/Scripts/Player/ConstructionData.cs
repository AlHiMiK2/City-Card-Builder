using UnityEngine.Events;

namespace _Project.Scripts.City
{
    public class ConstructionData
    {
        private int _health;
        private int _initEarn;
        private int _earn;
        
        public int Earn => _earn;
        public event UnityAction<int> HealthChanged; 

        public void SetData(int health, int earn)
        {
            _health = health;
            _initEarn = earn;
            _earn = _initEarn;
        }
        
        public void TakeDamage(int damage)
        {
            _health -= damage;

            if (_health < 0)
            {
                _health = 0;
                _earn = 0;
            }
            
            HealthChanged?.Invoke(_health);
        }
    }
}
