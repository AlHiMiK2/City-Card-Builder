using UnityEngine;

namespace _Project.Scripts.Card
{
    [CreateAssetMenu(fileName = "New Defence Card", menuName = "Create Defence Card", order = 0)]
    public class DefenceCardConfig : CardConfig
    {
        [SerializeField] private int _health;
        [SerializeField] private int _earn;
        [SerializeField] private Mesh _mesh;
        [SerializeField] private Material _material;
        [SerializeField] private Vector3 _meshOffset;
        [SerializeField] private Vector3 _rotationOffset;

        public int Health => _health;
        public int Earn => _earn;
        public Mesh Mesh => _mesh;
        public Material Material => _material;
        public Vector3 MeshOffset => _meshOffset;
        public Vector3 RotationOffset => _rotationOffset;
    }
}
