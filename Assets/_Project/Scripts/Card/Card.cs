using _Project.Scripts.City;
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
        private Camera _camera;
        private Transform _startParent;
        private Draggable _draggable;
        private bool _isInit;

        private void Awake()
        {
            _draggable = GetComponent<Draggable>();
        }

        public void Init(CardConfig config)
        {
            _config = config;
            _label.text = _config.Label;
            _icon.sprite = _config.Icon;
            _camera = Camera.main;
            _isInit = true;
        }

        private void OnEnable()
        {
            _draggable.EndDrag += Interact;
        }

        private void OnDisable()
        {
            _draggable.EndDrag -= Interact;
        }

        private void Interact()
        {
            if(!_isInit) return;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            bool isHit = Physics.Raycast(ray, out RaycastHit hitInfo, _rayDistance, _layerMask, QueryTriggerInteraction.Ignore);
            
            if (isHit)
            {
                if (_config is DefenceCardConfig constructionConfig)
                {
                    if (hitInfo.transform.TryGetComponent(out BuildPlace buildPlace))
                    {
                        //if (buildPlace.TryBuild(constructionConfig.ConstructionPrefab))
                        //{
                        //    Destroy(gameObject);
                        //}
                    }
                }
            }
        }
    }
}