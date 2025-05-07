using _Project.Scripts.Card;
using UnityEngine;

namespace _Project.Scripts
{
    [CreateAssetMenu(fileName = "New Game Config", menuName = "Create Game Config", order = 0)]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private int _walletCapacity;
        [Header("Card Generate")]
        [SerializeField] private int _upgradeCardCount;
        [SerializeField] private int _otherCardCount;
        [SerializeField] private CardDatabase _cardDatabase;

        public int WalletCapacity => _walletCapacity;
        public int UpgradeCardCount => _upgradeCardCount;
        public int OtherCardCount => _otherCardCount;
        public CardDatabase CardDatabase => _cardDatabase;
    }
}