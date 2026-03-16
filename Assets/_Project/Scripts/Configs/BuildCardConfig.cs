using UnityEngine;

namespace _Project.Scripts.Configs
{
    [CreateAssetMenu(fileName = "Build Card Config", menuName = "Create Build Card Config", order = 0)]
    public class BuildCardConfig : CardConfig
    {
        [SerializeField] private Build _buildPrefab;
        
        public Build BuildPrefab => _buildPrefab;
    }
}