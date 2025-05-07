using _Project.Scripts.Enums;
using UnityEngine;

namespace _Project.Scripts.Card
{
    [CreateAssetMenu(fileName = "New Attack Card", menuName = "Create Attack Card", order = 0)]
    public class AttackCardConfig : CardConfig
    {
        [SerializeField] private int _damage;
        [SerializeField] private DamageType _type;

        public int Damage => _damage;
        public DamageType Type => _type;
    }
}
