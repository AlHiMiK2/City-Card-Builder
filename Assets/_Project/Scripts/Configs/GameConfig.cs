using _Project.Scripts.Card;
using UnityEngine;

namespace _Project.Scripts
{
    [CreateAssetMenu(fileName = "New Game Config", menuName = "Create Game Config", order = 0)]
    public class GameConfig : ScriptableObject
    {
        [Header("Config")]
        [SerializeField] private int _walletCapacity;
        [SerializeField] private int _upgradeCardCount;
        [SerializeField] private int _otherCardCount;
        [SerializeField] private int _cardApplyPerTurn;
        [SerializeField] private CardDatabase _cardDatabase;

        public int WalletCapacity => _walletCapacity;
        public int UpgradeCardCount => _upgradeCardCount;
        public int OtherCardCount => _otherCardCount;
        public int CardApplyPerTurn => _cardApplyPerTurn;
        public CardDatabase CardDatabase => _cardDatabase;
    }
}