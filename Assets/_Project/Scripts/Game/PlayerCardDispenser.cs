using _Project.Scripts.Configs;
using _Project.Scripts.Game;
using _Project.Scripts.Handlers;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    [RequireComponent(typeof(CardHand))]
    public class PlayerCardDispenser : MonoBehaviour
    {
        [SerializeField] private Card _cardPrefab;
        [SerializeField] private ItemContainer _itemContainer;
        [SerializeField] private Slider _timerSlider;
        [SerializeField] private Ease _timerEase;
        
        private CardHand _cardHand;
        private Tween _timerTween;
        private float _dispenseRate;
        private CardConfig[] _cardConfigs;

        private void Start()
        {
            _cardHand = GetComponent<CardHand>();
            _dispenseRate = GameHandler.Instance.GameConfig.DispenseRate;
        }

        private void Update()
        {
            if (_itemContainer.IsFull) return;
            if (_timerTween != null) return;
            
            _timerTween = _timerSlider
                .DOValue(1f, _dispenseRate)
                .SetEase(_timerEase)
                .OnComplete(() =>
                {
                    var instance = Instantiate(_cardPrefab, _itemContainer.transform);
                    Vector3 targetPosition = _itemContainer.Spline.GetPoint(0.5f);
                    instance.transform.position = targetPosition;
                    instance.transform.right = _itemContainer.Spline.GetTangent(0.5f);
                
                    instance.Init(_cardHand.TakeCard(), GameHandler.Instance.Player);
                    instance.Draggable.Init(_itemContainer);
                    _timerSlider.value = 0f;
                    _timerTween = null;
                });
        }
    }
}