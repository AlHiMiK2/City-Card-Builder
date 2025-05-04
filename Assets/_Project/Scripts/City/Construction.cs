using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.City
{
    public class Construction : MonoBehaviour, IDamageable
    {
        [SerializeField] private int _health;

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
            Destroy(gameObject);
            Debug.Log(gameObject.name + " has been destroyed.");
        }
    }
}
