using _Project.Scripts.Card;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.City
{
    public class BuildPlace : MonoBehaviour
    {
        [SerializeField] private MeshFilter _constructionMeshFilter;
        [SerializeField] private MeshRenderer _constructionRenderer;
        [SerializeField] private Slider _healthBar;
        
        private ConstructionData _constructionData;
        private int _ownerIndex;
        private int _index;

        public int Index => _index;
        public int OwnerIndex => _ownerIndex;
        public ConstructionData ConstructionData => _constructionData;
        
        private void Awake()
        {
            _constructionData = new ConstructionData();
            _constructionData.HealthChanged += OnConstructionHealthChanged;
        }
        public void Init(int index, int ownerPlayerIndex, DefenceCardConfig defenceCardConfig)
        {
            _ownerIndex = ownerPlayerIndex;
            _index = index;
            Build(defenceCardConfig);
        }

        private void OnConstructionHealthChanged()
        {
            UpdateHealthBar();
            if (_constructionData.Health <= 0)
            {
                _constructionRenderer.material = null;
                _constructionMeshFilter.mesh = null;
                _constructionMeshFilter.transform.localPosition = Vector3.zero;
            }
        }

        private void UpdateHealthBar()
        {
            _healthBar.value = (float)_constructionData.Health / _constructionData.InitHealth;
        }
        
        public void Build(DefenceCardConfig config)
        {
            _constructionData.SetData(config.Health, config.Earn);
            _constructionRenderer.material = config.Material;
            _constructionMeshFilter.mesh = config.Mesh;
            _constructionMeshFilter.transform.localPosition = config.MeshOffset;
            _constructionMeshFilter.transform.SetLocalPositionAndRotation(config.MeshOffset, Quaternion.Euler(config.RotationOffset));
            UpdateHealthBar();
        }

        private void OnDestroy()
        {
            _constructionData.HealthChanged -= OnConstructionHealthChanged;
        }
    }
}