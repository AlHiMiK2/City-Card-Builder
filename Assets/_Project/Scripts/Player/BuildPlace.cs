using _Project.Scripts.Card;
using UnityEngine;

namespace _Project.Scripts.City
{
    public class BuildPlace : MonoBehaviour
    {
        [SerializeField] private MeshFilter _constructionMeshFilter;
        [SerializeField] private MeshRenderer _constructionRenderer;
        
        private ConstructionData _constructionData;
        private int _ownerIndex;

        public int OwnerIndex => _ownerIndex;
        public ConstructionData ConstructionData => _constructionData;
        
        private void Awake()
        {
            _constructionData = new ConstructionData();
            _constructionData.HealthChanged += OnConstructionHealthChanged;
        }
        public void Init(int ownerPlayerIndex, DefenceCardConfig defenceCardConfig)
        {
            _ownerIndex = ownerPlayerIndex;
            Build(defenceCardConfig);
        }

        private void OnConstructionHealthChanged(int health)
        {
            if (health <= 0)
            {
                _constructionRenderer.material = null;
                _constructionMeshFilter.mesh = null;
                _constructionMeshFilter.transform.localPosition = Vector3.zero;
            }
        }

        public void Build(DefenceCardConfig config)
        {
            _constructionData.SetData(config.Health, config.Earn);
            _constructionRenderer.material = config.Material;
            _constructionMeshFilter.mesh = config.Mesh;
            _constructionMeshFilter.transform.localPosition = config.MeshOffset;
        }

        private void OnDestroy()
        {
            _constructionData.HealthChanged -= OnConstructionHealthChanged;
        }
    }
}