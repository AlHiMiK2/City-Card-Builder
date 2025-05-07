namespace _Project.Scripts.City
{
    public class ConstructionData
    {
        private int _health;
        private int _earn;

        public void SetData(int health, int earn)
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
