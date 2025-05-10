using _Project.Scripts.City;
using _Project.Scripts.Handlers;
using _Project.Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Card
{
    [RequireComponent(typeof(Draggable))]
    public class Card : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] private TMP_Text _label;
        [SerializeField] private Image _icon;
        [Header("Interact")] 
        [SerializeField] private float _rayDistance;
        [SerializeField] private LayerMask _layerMask;
        
        private CardConfig _config;
        private CardHandler _cardHandler;
        private Camera _camera;
        private Transform _startParent;
        private Draggable _draggable;
        private int _ownerIndex;

        private void Awake()
        {
            _draggable = GetComponent<Draggable>();
        }

        public void Init(CardConfig config, int ownerPlayerIndex)
        {
            _config = config;
            _label.text = _config.Label;
            _icon.sprite = _config.Icon;
            _camera = Camera.main;
            _ownerIndex = ownerPlayerIndex;
            _cardHandler = CardHandler.Instance;
        }

        private void OnEnable()
        {
            _draggable.Drag += OnDrag;
            _draggable.EndDrag += OnEndDrag;
        }

        private void OnDisable()
        {
            _draggable.Drag -= OnDrag;
            _draggable.EndDrag -= OnEndDrag;
        }

        private void OnDrag()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            bool isHit = Physics.Raycast(ray, out RaycastHit hitInfo, _rayDistance, _layerMask, QueryTriggerInteraction.Ignore);
            
            if (isHit)
            {
                if (hitInfo.transform.TryGetComponent(out BuildPlace buildPlace) && buildPlace is not MainBuildPlace)
                {
                    _cardHandler.VisualiseApplyCard(_config, buildPlace, _ownerIndex);
                    return;
                }
            }
            
            _cardHandler.VisualiseApplyCard(_config, null, _ownerIndex);
        }
        
        private void OnEndDrag()
        {
            _cardHandler.VisualiseApplyCard(_config, null, _ownerIndex);
            
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            bool isHit = Physics.Raycast(ray, out RaycastHit hitInfo, _rayDistance, _layerMask, QueryTriggerInteraction.Ignore);
            
            if (isHit)
            {
                if (hitInfo.transform.TryGetComponent(out BuildPlace buildPlace))
                {
                    if(_cardHandler.TryApplyCard(_config, buildPlace, _ownerIndex))
                        Destroy(gameObject);
                }
            }
        }
    }
}