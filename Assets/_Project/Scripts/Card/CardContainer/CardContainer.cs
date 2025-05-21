using System.Collections.Generic;
using _Project.Scripts.Handlers;
using Mirror;
using UnityEngine;

namespace _Project.Scripts.Card
{
    public class CardContainer : NetworkBehaviour
    {
        [SerializeField] private Card _cardPrefab;
        [SerializeField] private Card _bonusCardPrefab;
        [SerializeField] private Transform _container;
        [SerializeField] private Vector3 _spawnOffset;
        
        private List<Card> _cards = new ();

        public List<Card> Cards => _cards;

        public void Fill(List<int> cardIds, int ownerPlayerIndex, bool isBonus)
        {
            foreach (var cardId in cardIds)
            {
                Card instance;
                
                if (isBonus)
                    instance = Instantiate(_bonusCardPrefab, _container);
                else
                    instance = Instantiate(_cardPrefab, _container);
                
                instance.Init(GameHandler.Instance.Config.CardDatabase.GetCardConfigById(cardId), ownerPlayerIndex);
                _cards.Add(instance);
                instance.transform.Translate(_spawnOffset);
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