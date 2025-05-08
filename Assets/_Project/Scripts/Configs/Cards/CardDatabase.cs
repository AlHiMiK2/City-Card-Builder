using UnityEngine;

namespace _Project.Scripts.Card
{
    [CreateAssetMenu(fileName = "New Card DB", menuName = "Create Card Database", order = 0)]
    public class CardDatabase : ScriptableObject
    {
        [SerializeField] private AttackCardConfig[] _attackCardConfigs;
        [SerializeField] private DefenceCardConfig _defaultDefenceCardConfig;
        [SerializeField] private DefenceCardConfig[] _defenceCardConfigs;
        [SerializeField] private UpgradeCardConfig[] _upgradeCardConfigs;
        [SerializeField] private DefenceCardConfig _mainDefenceCardConfig;

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
    }
}