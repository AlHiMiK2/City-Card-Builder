using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace _Project.Scripts
{
    public abstract class Build : MonoBehaviour
    {
        [Header("Main Settings")]
        [SerializeField] protected int MaxHealth;
        [Header("Effects")]
        [SerializeField] protected float _scaleDuration;
        [SerializeField] protected Ease _scaleEase;

        protected bool IsDestroyed;
        protected int Health;
        protected BaseBuildPlace Place;
        
        public event UnityAction<int, int> OnHealthChange;

        public void Init(BaseBuildPlace playerBuildPlace)
        {
            Place = playerBuildPlace;
        }

        private void Start()
        {
            Health = MaxHealth;
            OnHealthChange?.Invoke(Health, MaxHealth);
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            OnHealthChange?.Invoke(Health, MaxHealth);
            
            if (Health <= 0)
            {
                IsDestroyed = true;
                Place.ClearBuilding();
                transform.DOScale(Vector3.zero, _scaleDuration).SetEase(_scaleEase)
                    .OnComplete(Die);
            }
        }

        protected virtual void Die()
        {
            Destroy(gameObject);
        }
    }
}