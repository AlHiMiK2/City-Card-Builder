using _Project.Scripts.Handlers;
using UnityEngine;

namespace _Project.Scripts
{
    public class MainBuild : Build
    {
        [SerializeField] private int _earn;
        [SerializeField] private float _earnRate;
        [SerializeField] private ParticleSystem _earnEffect;

        private float _earnTime;
        
        private void Update()
        {
            if(IsDestroyed) return;
            if (Time.time >= _earnTime + _earnRate)
            {
                Place.Player.Wallet.AddMoney(_earn);
                //Instantiate(_earnEffect, transform.position, Quaternion.identity);
                _earnTime = Time.time;
            }
        }

        protected override void Die()
        {
            base.Die();
            
            if (Place.Player as Player)
            {
                GameHandler.Instance.PlayerDead();
            }
            if (Place.Player as EnemyAI)
            {
                GameHandler.Instance.EnemyDead();
            }
        }
    }
}