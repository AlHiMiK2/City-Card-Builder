using UnityEngine;

namespace _Project.Scripts.Configs
{
    [CreateAssetMenu(fileName = "Attack Card Config", menuName = "Create Attack Card Config", order = 0)]
    public class AttackCardConfig : CardConfig
    {
        [SerializeField] private int _damage;
        
        public int Damage => _damage;
    }
}