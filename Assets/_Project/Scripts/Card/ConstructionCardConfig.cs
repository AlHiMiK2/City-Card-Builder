using UnityEngine;

namespace _Project.Scripts.Card
{
    [CreateAssetMenu(fileName = "New Construction Card", menuName = "Create Construction Card", order = 0)]
    public class ConstructionCardConfig : CardConfig
    {
        [SerializeField] private GameObject _prefab;
        
        public GameObject Prefab => _prefab;
    }
}
