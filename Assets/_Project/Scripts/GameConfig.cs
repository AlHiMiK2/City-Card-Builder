using _Project.Scripts.City;
using UnityEngine;

namespace _Project.Scripts
{
    [CreateAssetMenu(fileName = "New Game Config", menuName = "Create Game Config", order = 0)]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private int _walletCapacity;
        [SerializeField] private int _maxCards;

        public int WalletCapacity => _walletCapacity;
        public int MaxCards => _maxCards;
    }
}