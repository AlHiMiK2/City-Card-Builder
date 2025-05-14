using _Project.Scripts.Card;
using _Project.Scripts.Handlers;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.City
{
    public class BuildPlace : MonoBehaviour
    {
        [SerializeField] private MeshFilter _constructionMeshFilter;
        [SerializeField] private MeshRenderer _constructionRenderer;
        [SerializeField] private MeshRenderer _buildPlaceRenderer;
        [SerializeField] private bool _isFlip;
        [SerializeField] private Vector3 _healthBarOffset;
        [SerializeField] private int _outlineLayerMask;
        
        private ConstructionData _constructionData;
        private int _ownerIndex;
        private int _index;
        private UIHandler _uiHandler;
        
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
            _uiHandler = UIHandler.Instance;
            _uiHandler.AddConstructionDataView(transform.position + _healthBarOffset, _ownerIndex);
            
            if (_isFlip)
            {
                _constructionRenderer.transform.localScale = new Vector3(1, 1, -1);
            }
            
            Build(defenceCardConfig);
        }

        private void OnConstructionHealthChanged()
        {
            UpdateHealthView();
            
            if (_constructionData.Health <= 0)
            {
                _constructionRenderer.material = null;
                _constructionRenderer.enabled = false;
                _constructionMeshFilter.mesh = null;
                _constructionMeshFilter.transform.localPosition = Vector3.zero;
            }
        }

        private void UpdateHealthView()
        {
            _uiHandler.SetConstructionDataViewValue(_constructionData.InitHealth, _constructionData.Health, _constructionData.Earn, _index, _ownerIndex);
        }
        
        public void Build(DefenceCardConfig config)
        {
            _constructionData.SetData(config.Health, config.Earn);
            _constructionRenderer.materials = config.Materials;
            _constructionRenderer.enabled = true;
            _constructionMeshFilter.mesh = config.Mesh;
            _constructionMeshFilter.transform.localPosition = config.MeshOffset;

            if (_isFlip)
            {
                var rotation = Quaternion.Euler(config.RotationOffset.x, config.RotationOffset.y + 180f, config.RotationOffset.z);
                _constructionMeshFilter.transform.SetLocalPositionAndRotation(config.MeshOffset, rotation);
            }
            else
            {
                _constructionMeshFilter.transform.SetLocalPositionAndRotation(config.MeshOffset, Quaternion.Euler(config.RotationOffset));
            }
        }

        public void SetOutlineState(bool state)
        {
            uint mask = _constructionRenderer.renderingLayerMask;
    
            if (state)
            {
                mask |= (uint)(1 << _outlineLayerMask);
            }
            else
            {
                mask &= ~(uint)(1 << _outlineLayerMask);
            }
    
            _constructionRenderer.renderingLayerMask = mask;
            _buildPlaceRenderer.renderingLayerMask = mask;
        }
        
        private void OnDestroy()
        {
            _constructionData.HealthChanged -= OnConstructionHealthChanged;
        }
    }
}