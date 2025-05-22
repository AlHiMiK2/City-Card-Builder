using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Card
{
    [CreateAssetMenu(fileName = "New Card DB", menuName = "Create Card Database", order = 0)]
    public class CardDatabase : ScriptableObject
    {
        [SerializeField] private DefenceCardConfig _defaultDefenceCardConfig;
        [SerializeField] private DefenceCardConfig _mainDefenceCardConfig;
        [SerializeField] private AttackCardConfig[] _attackCardConfigs;
        [SerializeField] private DefenceCardConfig[] _defenceCardConfigs;
        [SerializeField] private UpgradeCardConfig[] _upgradeCardConfigs;

        public DefenceCardConfig DefaultDefenceCardConfig => _defaultDefenceCardConfig;
        public DefenceCardConfig MainDefenceCardConfig => _mainDefenceCardConfig;

        public AttackCardConfig GetRandomAttackCardConfig()
        {
            return _attackCardConfigs[Random.Range(0, _attackCardConfigs.Length)];
        }
        
        public DefenceCardConfig GetRandomDefenceCardConfig()
        {
            return _defenceCardConfigs[Random.Range(0, _defenceCardConfigs.Length)]; 
        }
        
        public UpgradeCardConfig GetRandomUpgradeCardConfig()
        {
            return _upgradeCardConfigs[Random.Range(0, _upgradeCardConfigs.Length)];
        }

        public CardConfig GetCardConfigById(int id)
        {
            if (id == 0)
            {
                return _defaultDefenceCardConfig;
            }
            if (id == 1)
            {
                return _mainDefenceCardConfig;
            }
            foreach (var config in _attackCardConfigs)
            {
                if(config.Id == id)
                    return config;
            }
            foreach (var config in _defenceCardConfigs)
            {
                if(config.Id == id)
                    return config;
            }
            foreach (var config in _upgradeCardConfigs)
            {
                if(config.Id == id)
                    return config;
            }

            return null;
        }

        private void OnValidate()
        {
            _defaultDefenceCardConfig.SetId(0);
            _mainDefenceCardConfig.SetId(1);
            int id = 2;

            foreach (var config in _attackCardConfigs)
            {
                config.SetId(id);
                id++;
            }
            foreach (var config in _defenceCardConfigs)
            {
                config.SetId(id);
                id++;
            }
            foreach (var config in _upgradeCardConfigs)
            {
                config.SetId(id);
                id++;
            }
        }
    }
}