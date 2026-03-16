using _Project.Scripts.Handlers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace _Project.Scripts.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private float _dragSpeed;
        [SerializeField, Range(0, 100f)] private float _tiltSmoothness;
        [SerializeField, Range(0, 90f)] private float _maxTilt;
        
        private CanvasGroup _canvasGroup;
        private Canvas _canvas;
        private ItemContainer _container;
        private int _id;
        private bool _isDragging;
        private Vector3 _dragOffset;
        private Vector2 _targetPosition;
        
        public event UnityAction PointerEnter;
        public event UnityAction PointerExit;
        
        public CanvasGroup CanvasGroup => _canvasGroup;
        public ItemContainer Container => _container;
        public int ID => _id;
        public bool IsDragging => _isDragging;
        
        public void Init(ItemContainer container)
        {
            _container = container;
            _container.AddItem(this);
        }

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            _canvas = UIHandler.Instance.Canvas;
        }

        private void Update()
        {
            if (!_isDragging) return;
            Vector3 prevPos = transform.position;
            float moveDelta = _dragSpeed * _canvas.scaleFactor;
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, moveDelta * Time.deltaTime);
            Quaternion targetRot = Quaternion.Euler(0, 0, (prevPos.x - transform.position.x) / moveDelta / Time.deltaTime * _maxTilt);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, _tiltSmoothness * Time.deltaTime);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            transform.SetParent(_canvas.transform, true);
            transform.SetAsLastSibling();
            _canvasGroup.blocksRaycasts = false;
            _isDragging = true;
            _dragOffset = transform.position - Camera.main.ScreenToWorldPoint(eventData.position);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _targetPosition = Camera.main.ScreenToWorldPoint(eventData.position) + _dragOffset;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            transform.SetParent(_container.transform, true);
            _canvasGroup.blocksRaycasts = true;
            _isDragging = false;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            PointerEnter?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            PointerExit?.Invoke();
        }

        public void SetID(int id)
        {
            _id = id;
        }

        public void SetContainer(ItemContainer container)
        {
            _container.RemoveItem(_id);
            _container = container;
            _container.AddItem(this);
        }
    }
}