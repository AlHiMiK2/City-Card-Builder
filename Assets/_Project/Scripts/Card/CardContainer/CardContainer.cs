using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Card
{
    [RequireComponent(typeof(CardGenerator))]
    public class CardContainer : MonoBehaviour
    {
        private CardGenerator _generator;
        private List<Card> _cards;
        
        private void Awake()
        {
            _generator = GetComponent<CardGenerator>();
        }

        public void Fill()
        {
            _cards = _generator.Generate();
            
            foreach (var card in _cards)
            {
                card.transform.SetParent(transform);
            }
        }

        public void Clear()
        {
            _cards.Clear();
        }
    }
}