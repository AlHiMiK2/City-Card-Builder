using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

namespace _Project.Scripts.UI
{
    [RequireComponent(typeof(Card))]
    public class CardEffects : MonoBehaviour
    {
        [SerializeField] private float _spawnScaleDuration;
        [SerializeField] private Ease _spawnEase;
        [SerializeField] private float _usedScaleDuration;
        [SerializeField] private Ease _usedEase;
        [SerializeField] private float _pointerDuration;

        private Card _card;

        private void Awake()
        {
            _card = GetComponent<Card>();
        }

        private void OnEnable()
        {
            _card.OnUsed += CardUsed;
        }

        private void OnDisable()
        {
            _card.OnUsed -= CardUsed;
        }

        private void Start()
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, _spawnScaleDuration)
                .SetEase(_spawnEase)
                .OnComplete(() =>
                {
                    _card.Draggable.PointerEnter += OnPointerEnter;
                    _card.Draggable.PointerExit += OnPointerExit;
                });
        }

        private void OnPointerEnter()
        {
            transform.DOScale(Vector3.one * 1.1f, _pointerDuration);
        }
        
        private void OnPointerExit()
        {
            transform.DOScale(Vector3.one, _pointerDuration);
        }
        
        private void CardUsed()
        {
            _card.Draggable.PointerEnter -= OnPointerEnter;
            _card.Draggable.PointerExit -= OnPointerExit;
            _card.Draggable.CanvasGroup.interactable = false;
            transform.DOScale(Vector3.zero, _usedScaleDuration).SetEase(_usedEase).OnComplete(() =>
            {
                transform.DOKill();
                Destroy(gameObject);
            });
        }
    }
}