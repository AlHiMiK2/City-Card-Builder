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
        [SerializeField] private float _moveLenght;
        [SerializeField] private float _scaleDuration;
        [SerializeField] private RectTransform _view;
        
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
            _view.DOKill();
            _view.DOScale(Vector3.one * _dragScale, _scaleDuration);
            _isDragging = true;
        }
        
        private void OnDrag()
        {
            
        }
        
        private void OnEndDrag()
        {
            _view.DOKill();
            _view.DOScale(Vector3.one, _scaleDuration);
            _isDragging = false;
        }

        private void OnPointerEnter()
        {
            if (!_isDragging)
            {
                _view.DOKill();
                _view.DOScale(Vector3.one * _pointerEnterScale, _scaleDuration);
                _view.DOLocalMoveY(_moveLenght, _scaleDuration);
            }
        }

        private void OnPointerExit()
        {
            if (!_isDragging)
            {
                _view.DOKill();
                _view.DOScale(Vector3.one, _scaleDuration);
                _view.DOLocalMoveY(0, _scaleDuration);
            }
        }

        private void OnDestroy()
        {
            _view.DOKill();
        }
    }
}