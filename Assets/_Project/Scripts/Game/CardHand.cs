using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Configs;
using UnityEngine;

namespace _Project.Scripts.Game
{
    public class CardHand : MonoBehaviour
    {
        [SerializeField] private CardConfigDatabase _cardHand;

        private Queue<CardConfig> _cardQueue = new Queue<CardConfig>();
        
        private void Start()
        {
            FillHand();
        }

        public CardConfig TakeCard()
        {
            if (_cardQueue.Count == 0)
            {
                FillHand();
            }
            
            return _cardQueue.Dequeue();
        }

        private void FillHand()
        {
            List<CardConfig> tempList = _cardHand.GetConfigs().ToList();
            
            tempList.Shuffle();
        
            foreach (CardConfig card in tempList)
            {
                _cardQueue.Enqueue(card);
            }
        }
    }
}