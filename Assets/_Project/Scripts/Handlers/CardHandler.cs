using _Project.Scripts.Card;
using _Project.Scripts.City;
using UnityEngine;

namespace _Project.Scripts.Handlers
{
    public class CardHandler : MonoBehaviour
    {
        [SerializeField] private CardContainer _cardContainer;
        
        private CardBuildGenerator _cardBuildGenerator;
        private GameHandler _gameHandler;
        private int _appliedCardCount;
        
        public static CardHandler Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _gameHandler = GameHandler.Instance;
            _cardBuildGenerator = new CardBuildGenerator(_gameHandler.Config);
        }

        public void CreateCardBuild(int ownerPlayerIndex)
        {
            _cardContainer.Fill(_cardBuildGenerator.Generate(), ownerPlayerIndex);
            _appliedCardCount = 0;
        }

        public bool TryApplyCard(CardConfig config, BuildPlace target)
        {
            if (config is DefenceCardConfig defenceConfig)
            {
                target.Build(defenceConfig);
                Debug.Log("Applied Defence Card");
                CardApplied();
                return true;
            }
            if (config is AttackCardConfig attackConfig)
            {
                Debug.Log("Applied Attack Card");
                CardApplied();
                return true;
            }
            if (config is UpgradeCardConfig upgradeConfig)
            {
                Debug.Log("Applied Upgrade Card");
                CardApplied();
                return true;
            }

            return false;
        }

        private void CardApplied()
        {
            _appliedCardCount++;

            if (_gameHandler.Config.CardApplyPerTurn <= _appliedCardCount)
            {
                _cardContainer.Clear();
                _gameHandler.NextTurn();
            }
        }
    }
}