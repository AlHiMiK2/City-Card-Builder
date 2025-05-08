using _Project.Scripts.Handlers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace _Project.Scripts.UI
{
    public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        private Canvas _canvas;
        private RectTransform _rect;
        private Transform _startParent;
        
        public event UnityAction BeginDrag;
        public event UnityAction Drag;
        public event UnityAction EndDrag;
        public event UnityAction PointerEnter;
        public event UnityAction PointerExit;
        
        private void Awake()
        {
            _rect = GetComponent<RectTransform>();
        }

        private void Start()
        {
            _canvas = UIHandler.Instance.Canvas;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _startParent = transform.parent;
            _rect.SetParent(_canvas.transform, false);
            _rect.SetAsLastSibling();
            BeginDrag?.Invoke();
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
            Drag?.Invoke();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _rect.SetParent(_startParent);
            EndDrag?.Invoke();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            PointerEnter?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            PointerExit?.Invoke();
        }
    }
}