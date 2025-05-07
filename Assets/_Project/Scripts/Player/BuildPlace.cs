using _Project.Scripts.Card;
using _Project.Scripts.Handlers;
using UnityEngine;

namespace _Project.Scripts.City
{
    public class BuildPlace : MonoBehaviour
    {
        [SerializeField] private MeshFilter _constructionMeshFilter;
        [SerializeField] private MeshRenderer _constructionRenderer;
        
        private bool _isActive;
        private Construction _construction;

        public bool IsActive => _isActive;
        public Construction Construction => _construction;

        private void Awake()
        {
            _construction = new Construction(this);
        }

        private void Start()
        {
            Build(GameHandler.Instance.Config.CardDatabase.DefaultDefenceCardConfig);
        }

        public void Build(DefenceCardConfig config)
        {
            _construction.Init(config.Health, config.Earn);
            _constructionRenderer.material = config.Material;
            _constructionMeshFilter.mesh = config.Mesh;
            Debug.Log(config.Health + " " + config.Earn);
        }

        public void Enable()
        {
            _isActive = true;
        }

        public void Disable()
        {
            _isActive = false;
        }
    }
}