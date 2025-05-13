using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Card
{
    public class CardBuildGenerator
    {
        private GameConfig _config;

        public CardBuildGenerator(GameConfig config)
        {
            _config = config;
        }

        public List<CardConfig> Generate(bool isBonusBuild)
        {
            List<CardConfig> instantiatedCards = new List<CardConfig>();

            if (isBonusBuild == false)
            {
                for (int i = 0; i < _config.UpgradeCardCount; i++)
                {
                    instantiatedCards.Add(_config.CardDatabase.GetRandomUpgradeCardConfig());
                }
            }

            int otherCardCount = isBonusBuild ? _config.BonusCardCount : _config.OtherCardCount;

            for (int i = 0; i < otherCardCount; i++)
            {
                int selectedCardType = Random.Range(0, 2);

                if (selectedCardType == 0)
                {
                    instantiatedCards.Add(_config.CardDatabase.GetRandomAttackCardConfig());
                }
                else
                {
                    instantiatedCards.Add(_config.CardDatabase.GetRandomDefenceCardConfig());
                }
            }

            return instantiatedCards;
        }
    }
}