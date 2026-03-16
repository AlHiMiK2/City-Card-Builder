using _Project.Scripts.Game;
using _Project.Scripts.Handlers;
using UnityEngine;

namespace _Project.Scripts.UI
{
    [RequireComponent(typeof(CardHand))]
    public class EnemyCardDispenser : MonoBehaviour
    {
        [SerializeField] private EnemyCardContainer _cardContainer;
        
        private float _dispenseRate;
        private CardHand _cardHand;
        private float _dispenseTimer;

        private void Start()
        {
            _cardHand = GetComponent<CardHand>();
            _dispenseRate = GameHandler.Instance.GameConfig.DispenseRate;
        }

        private void Update()
        {
            if (_cardContainer.IsFull) return;
            
            _dispenseTimer += Time.deltaTime;
            
            if (_dispenseTimer >= _dispenseRate)
            {
                _cardContainer.AddConfig(_cardHand.TakeCard());
                _dispenseTimer = 0;
            }
        }
    }
}