using _Project.Scripts.UI;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Card
{
    [RequireComponent(typeof(Card))]
    [RequireComponent(typeof(Draggable))]
    public class CardEffect : MonoBehaviour
    {
        [SerializeField] private float _pointerEnterScale;
        [SerializeField] private float _dragScale;
        [SerializeField] private float _scaleDuration;
        
        private Draggable _draggable;
        private Card _card;
        private bool _isDragging;
        
        private void Awake()
        {
            _draggable = GetComponent<Draggable>();
            _card = GetComponent<Card>();
        }

        private void OnEnable()
        {
            _draggable.BeginDrag += OnBeginDrag;
            _draggable.Drag += OnDrag;
            _draggable.EndDrag += OnEndDrag;
            _draggable.PointerEnter += OnPointerEnter;
            _draggable.PointerExit += OnPointerExit;
        }

        private void OnDisable()
        {
            _draggable.BeginDrag -= OnBeginDrag;
            _draggable.Drag -= OnDrag;
            _draggable.EndDrag -= OnEndDrag;
            _draggable.PointerEnter -= OnPointerEnter;
            _draggable.PointerExit -= OnPointerExit;
        }

        private void OnBeginDrag()
        {
            transform.DOKill();
            transform.DOScale(Vector3.one * _dragScale, _scaleDuration);
            _isDragging = true;
        }
        
        private void OnDrag()
        {
            
        }
        
        private void OnEndDrag()
        {
            transform.DOKill();
            transform.DOScale(Vector3.one, _scaleDuration);
            _isDragging = false;
        }

        private void OnPointerEnter()
        {
            if (!_isDragging)
            {
                transform.DOKill();
                transform.DOScale(Vector3.one * _pointerEnterScale, _scaleDuration);
            }
        }

        private void OnPointerExit()
        {
            if (!_isDragging)
            {
                transform.DOKill();
                transform.DOScale(Vector3.one, _scaleDuration);
            }
        }

        private void OnDestroy()
        {
            transform.DOKill();
        }
    }
}