using _Project.Scripts.Configs;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts
{
    public abstract class BaseBuildPlace : MonoBehaviour
    {
        [SerializeField] private Transform _buildPlace;
        
        private Build _build;
        
        public BasePlayer Player { get; private set; }
        
        public void Init(BasePlayer player)
        {
            Player = player;    
        }
        
        public void Build(BuildCardConfig config)
        {
            if (_build)
                Destroy(_build.gameObject);
                    
            var instance = Instantiate(config.BuildPrefab, transform);
            instance.transform.position = _buildPlace.position;
            instance.Init(this);
            _build = instance;
            transform.DOPunchScale(Vector3.one * 0.25f, 0.2f).OnComplete(() =>
            {
                transform.localScale = Vector3.one;
            });
        }

        public void Attack(int damage)
        {
            if(_build)
                _build.TakeDamage(damage);
            transform.DOPunchScale(Vector3.one * 0.25f, 0.2f).OnComplete(() =>
            {
                transform.localScale = Vector3.one;
            });
        }

        public void ClearBuilding()
        {
            _build = null;
        }
    }
}