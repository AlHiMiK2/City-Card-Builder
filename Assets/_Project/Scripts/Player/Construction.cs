using UnityEngine;

namespace _Project.Scripts.City
{
    public class Construction
    {
        private int _health;
        private int _earn;
        private BuildPlace _buildPlace;

        public Construction(BuildPlace buildPlace)
        {
            _buildPlace = buildPlace;
        }

        public void Init(int health, int earn)
        {
            _health = health;
            earn = _earn;
        }
        
        public void TakeDamage(int damage)
        {
            _health -= damage;

            if (_health < 0)
            {
                _health = 0;
                Die();    
            }
        }

        private void Die()
        {
        }
    }
}
