using System.Collections.Generic;
using _Project.Scripts.Handlers;
using UnityEngine;

namespace _Project.Scripts.Card
{
    public class CardGenerator : MonoBehaviour
    {
        [SerializeField] private Card _cardPrefab;
        
        private GameConfig _config;

        private void Start()
        {
            _config = GameHandler.Instance.Config;
        }

        public List<Card> Generate()
        {
            List<Card> instantiatedCards = new List<Card>();

            for (int i = 0; i < _config.UpgradeCardCount; i++)
            {
                var instance = Instantiate(_cardPrefab);
                instance.Init(_config.CardDatabase.GetRandomUpgradeCardConfig());
                instantiatedCards.Add(instance);
            }

            for (int i = 0; i < _config.OtherCardCount; i++)
            {
                int selectedCardType = Random.Range(0, 2);
                var instance = Instantiate(_cardPrefab);

                if (selectedCardType == 0)
                {
                    instance.Init(_config.CardDatabase.GetRandomAttackCardConfig());
                }
                else
                {
                    instance.Init(_config.CardDatabase.GetRandomDefenceCardConfig());
                }
                
                instantiatedCards.Add(instance);
            }

            return instantiatedCards;
        }
    }
}