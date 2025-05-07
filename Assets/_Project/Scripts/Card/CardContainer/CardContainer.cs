using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Card
{
    public class CardContainer : MonoBehaviour
    {
        [SerializeField] private Card _cardPrefab;
        
        private List<Card> _cards = new ();

        public List<Card> Cards => _cards;

        public void Fill(List<CardConfig> cardConfigs, int ownerPlayerIndex)
        {
            foreach (var cardConfig in cardConfigs)
            {
                var instance = Instantiate(_cardPrefab, transform);
                instance.Init(cardConfig, ownerPlayerIndex);
                _cards.Add(instance);
            }
        }

        public void Clear()
        {
            foreach (var card in _cards)
            {
                if(card)
                    Destroy(card.gameObject);
            }
            
            _cards.Clear();
        }
    }
}