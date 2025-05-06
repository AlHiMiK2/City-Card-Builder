using UnityEngine;

namespace _Project.Scripts.Card
{
    [CreateAssetMenu(fileName = "New Construction Card", menuName = "Create Construction Card", order = 0)]
    public class ConstructionCardConfig : CardConfig
    {
        [SerializeField] private int _startHealth;
        [SerializeField] private int _earn;
        [SerializeField] private Mesh _mesh;
        [SerializeField] private Material _material;

        public int StartHealth => _startHealth;
        public int Earn => _earn;
        public Mesh Mesh => _mesh;
        public Material Material => _material;
    }
}
