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

        public List<int> Generate(bool isBonusBuild)
        {
            List<int> instantiatedCards = new List<int>();

            if (isBonusBuild == false)
            {
                for (int i = 0; i < _config.UpgradeCardCount; i++)
                {
                    instantiatedCards.Add(_config.CardDatabase.GetRandomUpgradeCardConfig().Id);
                }
            }

            int otherCardCount = isBonusBuild ? _config.BonusCardCount : _config.OtherCardCount;

            for (int i = 0; i < otherCardCount; i++)
            {
                int selectedCardType = Random.Range(0, 2);

                if (selectedCardType == 0)
                {
                    instantiatedCards.Add(_config.CardDatabase.GetRandomAttackCardConfig().Id);
                }
                else
                {
                    instantiatedCards.Add(_config.CardDatabase.GetRandomDefenceCardConfig().Id);
                }
            }

            return instantiatedCards;
        }
    }
}