using UnityEngine;

namespace _Project.Scripts.Configs
{
    [CreateAssetMenu(fileName = "New Game Config", menuName = "Create Game Config", order = 0)]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private int _startMoneyCount;
        [SerializeField] private float _dispenseRate;
        [SerializeField] private int _maxCardCount;
        [SerializeField] private BuildCardConfig _mainBuild;
        
        public int StartMoneyCount => _startMoneyCount;
        public float DispenseRate => _dispenseRate;
        public int MaxCardCount => _maxCardCount;
        public BuildCardConfig MainBuild => _mainBuild;
    }
}