using System.Collections.Generic;
using _Project.Scripts.Configs;
using _Project.Scripts.Handlers;
using UnityEngine;

namespace _Project.Scripts.Game
{
    public class EnemyCardContainer : MonoBehaviour
    {
        private int _capacity;
        private List<CardConfig> _cardConfigs = new List<CardConfig>();
    
        public List<CardConfig> CardConfigs => _cardConfigs;
        public bool IsFull => _cardConfigs.Count >= _capacity;

        private void Start()
        {
            _capacity = GameHandler.Instance.GameConfig.MaxCardCount;
        }

        public void AddConfig(CardConfig config)
        {
            _cardConfigs.Add(config);
        }

        public void RemoveConfig(CardConfig config)
        {
            _cardConfigs.Remove(config);
        }
    }
}